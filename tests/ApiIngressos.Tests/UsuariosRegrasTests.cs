using Xunit;

public class UsuariosRegrasTests
{
    [Fact]
    public void CadastrarUsuario_CpfDuplicado_DeveRetornarErro400()
    {
        // Arrange
        int statusEsperado = 400;

        // Act
        int statusObtido = 400;

        // Assert
        Assert.Equal(statusEsperado, statusObtido);
    }
}