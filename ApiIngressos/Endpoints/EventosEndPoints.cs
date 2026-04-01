using Dapper;
using System.Data;
using ApiIngressos.Models;

namespace ApiIngressos.Endpoints;

public static class EventosEndpoints
{
    public static void MapEventosEndpoints(this WebApplication app)
    {
        // POST - Criar evento
        app.MapPost("/api/eventos", async (Evento evento, IDbConnection db) =>
        {
            var sql = @"
                INSERT INTO Eventos
                (Nome, CapacidadeTotal, DataEvento, PrecoPadrao)
                VALUES
                (@Nome, @CapacidadeTotal, @DataEvento, @PrecoPadrao)
            ";

            await db.ExecuteAsync(sql, evento);

            return Results.Ok(evento);
        });

        // GET - Listar eventos
        app.MapGet("/api/eventos", async (IDbConnection db) =>
        {
            var sql = "SELECT Id, Nome, CapacidadeTotal, DataEvento, PrecoPadrao FROM Eventos";

            var eventos = await db.QueryAsync<Evento>(sql);

            return Results.Ok(eventos);
        });
    }
}
