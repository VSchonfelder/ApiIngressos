using Dapper;
using System.Data;
using ApiIngressos.Models;

namespace ApiIngressos.Endpoints;

public static class UsuariosEndpoints
{
    public static void MapUsuariosEndpoints(this WebApplication app)
    {
        app.MapPost("/api/usuarios", async (Usuario usuario, DbConnectionFactory factory) =>
        {
            using var db = factory.CreateConnection();
            
            var sqlVerificar = "SELECT COUNT(*) FROM Usuarios WHERE Cpf = @Cpf";

            var quantidade = await db.ExecuteScalarAsync<int>(
                sqlVerificar,
                new { usuario.Cpf }
            );

            if (quantidade > 0)
            {
                return Results.BadRequest("CPF já cadastrado.");
            }

            var sqlInserir = @"
                INSERT INTO Usuarios (Cpf, Nome, Email)
                VALUES (@Cpf, @Nome, @Email)
            ";

            await db.ExecuteAsync(sqlInserir, usuario);

            return Results.Ok(usuario);
        });

        app.MapGet("/api/usuarios", async (DbConnectionFactory factory) =>
{
    using var db = factory.CreateConnection();

    var sql = "SELECT Cpf, Nome, Email FROM Usuarios";

    var usuarios = await db.QueryAsync<Usuario>(sql);

    return Results.Ok(usuarios);
});
    }
}
