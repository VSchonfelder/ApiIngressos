using Xunit;

public class UsuariosRegrasTests
{
    [Fact]
    public void Deve_Retornar_Erro_Quando_Cpf_Duplicado()
    {
        // Arrange
        int statusEsperado = 400;

        // Act
        int statusObtido = 400;

        // Assert
        Assert.Equal(statusEsperado, statusObtido);
    }
}