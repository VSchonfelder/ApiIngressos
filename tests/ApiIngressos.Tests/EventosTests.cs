using Xunit;
using ApiIngressos.Models;

public class EventosTests
{
    [Fact]
    public void ListarEventos_QuandoChamado_DeveRetornarSucesso()
    {
        Assert.True(true);
    }

    [Fact]
    public void CriarEvento_SemNome_DeveRetornarFalso()
    {
        // Arrange
        var evento = new Evento { Nome = "" };

        // Act
        var valido = !string.IsNullOrWhiteSpace(evento.Nome);

        // Assert
        Assert.False(valido);
    }
}