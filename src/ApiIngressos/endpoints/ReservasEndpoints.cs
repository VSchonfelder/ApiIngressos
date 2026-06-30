using Dapper;
using System.Data;
using ApiIngressos.Models;

namespace ApiIngressos.Endpoints;

public static class ReservasEndpoints
{
    public static void MapReservasEndpoints(this WebApplication app)
    {
        app.MapPost("/api/reservas", async (Reserva reserva, DbConnectionFactory factory) =>
        {
            using var db = factory.CreateConnection();

            var evento = await db.QueryFirstOrDefaultAsync<Evento>(
                "SELECT Id, DataEvento, CapacidadeTotal, PrecoPadrao FROM Eventos WHERE Id = @Id",
                new { Id = reserva.EventoId }
            );

            if (evento is null)
                return Results.BadRequest("Evento não encontrado.");

            if (evento.DataEvento < DateTime.UtcNow)
                return Results.BadRequest("Não é possível reservar ingresso para um evento que já ocorreu.");

            var totalReservas = await db.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Reservas WHERE EventoId = @EventoId",
                new { EventoId = reserva.EventoId }
            );

            if (totalReservas >= evento.CapacidadeTotal)
                return Results.BadRequest("Não há vagas disponíveis para este evento.");

            var reservaExistente = await db.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Reservas WHERE EventoId = @EventoId AND UsuarioCpf = @UsuarioCpf",
                new { EventoId = reserva.EventoId, UsuarioCpf = reserva.UsuarioCpf }
            );

            if (reservaExistente > 0)
                return Results.BadRequest("Usuário já possui uma reserva para este evento.");

            if (!string.IsNullOrWhiteSpace(reserva.CupomUtilizado))
            {
                var cupom = await db.QueryFirstOrDefaultAsync<Cupom>(
                    "SELECT Codigo, PorcentagemDesconto, ValorMinimoRegra FROM Cupons WHERE Codigo = @Codigo",
                    new { Codigo = reserva.CupomUtilizado }
                );

                if (cupom is null)
                    return Results.BadRequest("Cupom informado não existe.");

                if (reserva.ValorFinalPago < cupom.ValorMinimoRegra)
                    return Results.BadRequest($"O cupom exige valor mínimo de R$ {cupom.ValorMinimoRegra:F2}.");

                var valorEsperado = evento.PrecoPadrao * (1 - cupom.PorcentagemDesconto / 100m);
                if (Math.Abs(reserva.ValorFinalPago - valorEsperado) > 0.01m)
                    return Results.BadRequest($"Valor final incorreto. Com o cupom, o valor esperado é R$ {valorEsperado:F2}.");
            }
            else
            {
                if (reserva.ValorFinalPago != evento.PrecoPadrao)
                    return Results.BadRequest($"Valor final deve ser R$ {evento.PrecoPadrao:F2} para este evento.");
            }

            var sql = @"
                INSERT INTO Reservas (UsuarioCpf, EventoId, CupomUtilizado, ValorFinalPago)
                VALUES (@UsuarioCpf, @EventoId, @CupomUtilizado, @ValorFinalPago)
                RETURNING Id
            ";

            var id = await db.ExecuteScalarAsync<int>(sql, reserva);
            reserva.Id = id;

            return Results.Ok(reserva);
        });

        app.MapGet("/api/reservas", async (DbConnectionFactory factory) =>
        {
            using var db = factory.CreateConnection();

            var sql = @"
                SELECT
                    r.Id,
                    r.ValorFinalPago,
                    u.Cpf        AS UsuarioCpf,
                    u.Nome       AS UsuarioNome,
                    e.Id         AS EventoId,
                    e.Nome       AS EventoNome,
                    e.DataEvento AS EventoData,
                    c.Codigo     AS CupomCodigo,
                    c.PorcentagemDesconto AS CupomDesconto
                FROM Reservas r
                INNER JOIN Usuarios u ON u.Cpf      = r.UsuarioCpf
                INNER JOIN Eventos  e ON e.Id        = r.EventoId
                LEFT  JOIN Cupons   c ON c.Codigo    = r.CupomUtilizado
            ";

            var reservas = await db.QueryAsync(sql);
            return Results.Ok(reservas);
        });

        app.MapGet("/api/reservas/{id}", async (int id, DbConnectionFactory factory) =>
        {
            using var db = factory.CreateConnection();

            var sql = @"
                SELECT
                    r.Id,
                    r.ValorFinalPago,
                    u.Cpf        AS UsuarioCpf,
                    u.Nome       AS UsuarioNome,
                    e.Id         AS EventoId,
                    e.Nome       AS EventoNome,
                    e.DataEvento AS EventoData,
                    c.Codigo     AS CupomCodigo,
                    c.PorcentagemDesconto AS CupomDesconto
                FROM Reservas r
                INNER JOIN Usuarios u ON u.Cpf   = r.UsuarioCpf
                INNER JOIN Eventos  e ON e.Id     = r.EventoId
                LEFT  JOIN Cupons   c ON c.Codigo = r.CupomUtilizado
                WHERE r.Id = @Id
            ";

            var reserva = await db.QueryFirstOrDefaultAsync(sql, new { Id = id });

            if (reserva is null)
                return Results.NotFound("Reserva não encontrada.");

            return Results.Ok(reserva);
        });

        app.MapDelete("/api/reservas/{id}", async (int id, DbConnectionFactory factory) =>
        {
            using var db = factory.CreateConnection();

            var sql = "DELETE FROM Reservas WHERE Id = @Id";
            var linhasAfetadas = await db.ExecuteAsync(sql, new { Id = id });

            if (linhasAfetadas == 0)
                return Results.NotFound("Reserva não encontrada.");

            return Results.Ok("Reserva removida com sucesso.");
        });
    }
}
