using Dapper;
using System.Data;
using ApiIngressos.Models;

namespace ApiIngressos.Endpoints;

public static class UsuariosEndpoints
{
    public static void MapUsuariosEndpoints(this WebApplication app)
    {
        app.MapPost("/api/usuarios", async (Usuario usuario, IDbConnection db) =>
        {
            // Verifica se já existe um usuário com o CPF informado
            var sqlVerificar = "SELECT COUNT(*) FROM Usuarios WHERE Cpf = @Cpf";

            var quantidade = await db.ExecuteScalarAsync<int>(
                sqlVerificar,
                new { usuario.Cpf }
            );

            if (quantidade > 0)
            {
                return Results.BadRequest("CPF já cadastrado.");
            }

            // Insere o usuário no banco
            var sqlInserir = @"
                INSERT INTO Usuarios (Cpf, Nome, Email)
                VALUES (@Cpf, @Nome, @Email)
            ";

            await db.ExecuteAsync(sqlInserir, usuario);

            return Results.Ok(usuario);
        });
    }
}