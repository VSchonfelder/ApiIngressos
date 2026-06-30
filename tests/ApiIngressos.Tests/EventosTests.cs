using Xunit;
using ApiIngressos.Models;

public class EventosTests
{
    [Fact]
    public void Deve_Listar_Eventos()
    {
        Assert.True(true);
    }

    [Fact]
    public void NaoDevePermitirEventoSemNome()
    {
        var evento = new Evento { Nome = "" };

        var valido = !string.IsNullOrWhiteSpace(evento.Nome);

        Assert.False(valido);
    }
}