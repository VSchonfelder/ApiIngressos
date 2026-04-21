namespace IngressosApp.Services;

public class Cupom
{
    public string Codigo { get; set; } = string.Empty;
    public decimal PorcentagemDesconto { get; set; }
    public decimal ValorMinimoRegra { get; set; }
}

public class CupomService
{
    private List<Cupom> _cupons = new();

    public List<Cupom> GetCupons() => _cupons;

    public string AdicionarCupom(Cupom cupom)
    {
        if (_cupons.Any(c => c.Codigo == cupom.Codigo))
            return $"Erro: já existe um cupom cadastrado com o código '{cupom.Codigo}'.";

        _cupons.Add(cupom);
        return "ok";
    }

    public void RemoverCupom(Cupom cupom)
    {
        _cupons.Remove(cupom);
    }
}