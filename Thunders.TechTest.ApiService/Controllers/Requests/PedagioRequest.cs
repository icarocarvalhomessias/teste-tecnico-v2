namespace Thunders.TechTest.ApiService.Controllers.Requests;

public record PedagioRequest
{
    public string Nome { get; set; }
    public decimal Valor { get; set; }
    public Guid CidadeId { get; set; }

    public PedagioRequest(string nome, decimal valor, Guid cidadeId)
    {
        Nome = nome;
        Valor = valor;
        CidadeId = cidadeId;
    }
}
