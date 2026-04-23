using Dapper;
using System.Data;
using ApiIngressos.Models;

namespace ApiIngressos.Endpoints;

public static class CuponsEndpoints
{
    public static void MapCuponsEndpoints(this WebApplication app)
    {
        app.MapPost("/api/cupons", async (Cupom cupom, DbConnectionFactory factory) =>
        {
            using var db = factory.CreateConnection();
            
            var sql = @"
                INSERT INTO Cupons
                (Codigo, PorcentagemDesconto, ValorMinimoRegra)
                VALUES
                (@Codigo, @PorcentagemDesconto, @ValorMinimoRegra)
            ";

            await db.ExecuteAsync(sql, cupom);

            return Results.Ok(cupom);
        });

        app.MapGet("/api/cupons", async (DbConnectionFactory factory) =>
        {
            using var db = factory.CreateConnection();

            var sql = @"
                SELECT Codigo, PorcentagemDesconto, ValorMinimoRegra
                FROM Cupons
            ";

            var cupons = await db.QueryAsync<Cupom>(sql);

            return Results.Ok(cupons);
        });
    }
}
