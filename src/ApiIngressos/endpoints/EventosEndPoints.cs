using Dapper;
using System.Data;
using ApiIngressos.Models;

namespace ApiIngressos.Endpoints;

public static class EventosEndpoints
{
    public static void MapEventosEndpoints(this WebApplication app)
    {
        app.MapPost("/api/eventos", async (Evento evento, DbConnectionFactory factory) =>
        {
            using var db = factory.CreateConnection();

            if (string.IsNullOrWhiteSpace(evento.Nome))
                return Results.BadRequest("O nome do evento não pode ser vazio.");

            if (evento.DataEvento <= DateTime.UtcNow)
                return Results.BadRequest("A data do evento deve ser uma data futura.");

            if (evento.CapacidadeTotal <= 0)
                return Results.BadRequest("A capacidade total deve ser maior que zero.");

            if (evento.PrecoPadrao <= 0)
                return Results.BadRequest("O preço padrão deve ser maior que zero.");

            var sql = @"
                INSERT INTO Eventos
                (Nome, CapacidadeTotal, DataEvento, PrecoPadrao)
                VALUES
                (@Nome, @CapacidadeTotal, @DataEvento, @PrecoPadrao)
            ";

            await db.ExecuteAsync(sql, evento);

            return Results.Ok(evento);
        });

        app.MapGet("/api/eventos", async (DbConnectionFactory factory) =>
        {
            using var db = factory.CreateConnection();

            var sql = "SELECT Id, Nome, CapacidadeTotal, DataEvento, PrecoPadrao FROM Eventos";

            var eventos = await db.QueryAsync<Evento>(sql);

            return Results.Ok(eventos);
        });

        app.MapDelete("/api/eventos/{id}", async (int id, DbConnectionFactory factory) =>
        {
            using var db = factory.CreateConnection();

            var temReservas = await db.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Reservas WHERE EventoId = @Id",
                new { Id = id }
            );

            if (temReservas > 0)
                return Results.BadRequest("Evento não pode ser removido pois possui reservas vinculadas.");

            var linhasAfetadas = await db.ExecuteAsync(
                "DELETE FROM Eventos WHERE Id = @Id",
                new { Id = id }
            );

            if (linhasAfetadas == 0)
                return Results.NotFound("Evento não encontrado.");

            return Results.Ok("Evento removido com sucesso.");
        });
        app.MapPatch("/api/eventos/{id}/adiar", async (int id, NovaDataDto dto, DbConnectionFactory factory) =>
        {
            using var db = factory.CreateConnection();

            var evento = await db.QueryFirstOrDefaultAsync<Evento>(
                "SELECT Id, DataEvento FROM Eventos WHERE Id = @Id",
                new { Id = id }
            );

            if (evento is null)
                return Results.NotFound("Evento não encontrado.");

            if (evento.DataEvento <= DateTime.UtcNow)
                return Results.BadRequest("Não é possível adiar um evento que já ocorreu.");

            if (dto.NovaData <= DateTime.UtcNow)
                return Results.BadRequest("A nova data do evento deve ser no futuro.");

            if (dto.NovaData <= evento.DataEvento)
                return Results.BadRequest("A nova data deve ser estritamente posterior à data original.");

            var sqlUpdate = "UPDATE Eventos SET DataEvento = @NovaData WHERE Id = @Id";
            await db.ExecuteAsync(sqlUpdate, new { NovaData = dto.NovaData, Id = id });

            return Results.Ok("Evento adiado com sucesso.");
        });

        app.MapGet("/api/eventos/{id}/relatorio-vendas", async (int id, DbConnectionFactory factory) =>
        {
            using var db = factory.CreateConnection();
            
            var eventoExiste = await db.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Eventos WHERE Id = @Id", new { Id = id });

            if (eventoExiste == 0)
                return Results.NotFound("Evento não encontrado.");

            var sql = @"
                SELECT 
                    e.Nome AS NomeEvento,
                    e.CapacidadeTotal,
                    COUNT(r.Id) AS IngressosVendidos,
                    (e.CapacidadeTotal - COUNT(r.Id)) AS VagasDisponiveis,
                    COALESCE(SUM(r.ValorFinalPago), 0) AS FaturamentoTotal
                FROM Eventos e
                LEFT JOIN Reservas r ON e.Id = r.EventoId
                WHERE e.Id = @Id
                GROUP BY e.Id, e.Nome, e.CapacidadeTotal
            ";

            var relatorio = await db.QueryFirstOrDefaultAsync(sql, new { Id = id });

            return Results.Ok(relatorio);
        });
    }
}

public record NovaDataDto(DateTime NovaData);
