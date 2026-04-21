using Dapper;
using System.Data;
using ApiIngressos.Models;

namespace ApiIngressos.Endpoints;

public static class CuponsEndpoints
{
    public static void MapCuponsEndpoints(this WebApplication app)
    {
        app.MapPost("/api/cupons", async (Cupom cupom, IDbConnection db) =>
        {
            var sql = @"
                INSERT INTO Cupons
                (Codigo, PorcentagemDesconto, ValorMinimoRegra)
                VALUES
                (@Codigo, @PorcentagemDesconto, @ValorMinimoRegra)
            ";

            await db.ExecuteAsync(sql, cupom);

            return Results.Ok(cupom);
        });
    }
}