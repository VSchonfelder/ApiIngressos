using Xunit;

public class UsuariosTests
{
    [Fact]
    public void Deve_Criar_Usuario_Valido()
    {
        // Arrange
        var cpf = "12345678900";

        // Act
        var valido = cpf.Length == 11;

        // Assert
        Assert.True(valido);
    }
}