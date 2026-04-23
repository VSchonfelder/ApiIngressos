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
        }
        
        );

        app.MapDelete("/api/eventos/{id}", async (int id, DbConnectionFactory factory) =>
{
        using var db = factory.CreateConnection();

        var sql = "DELETE FROM Eventos WHERE Id = @Id";

        var linhasAfetadas = await db.ExecuteAsync(sql, new { Id = id });

        if (linhasAfetadas == 0)
        return Results.NotFound("Evento não encontrado");

    return Results.Ok("Evento removido com sucesso");
});
    }
}
