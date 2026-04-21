namespace IngressosApp.Services;

public class Evento
{
    public string Nome { get; set; } = "";
    public DateTime Data { get; set; } = DateTime.Today;
    public string Local { get; set; } = "";
}

public class EventoService
{
    private List<Evento> _eventos = new();

    public List<Evento> GetEventos() => _eventos;

    public string AdicionarEvento(Evento evento)
    {
        if (_eventos.Any(e => e.Nome == evento.Nome))
            return $"Erro: já existe um evento cadastrado com o nome '{evento.Nome}'.";

        _eventos.Add(evento);
        return "ok";
    }

    public void RemoverEvento(Evento evento)
    {
        _eventos.Remove(evento);
    }
}