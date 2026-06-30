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
    }
}
