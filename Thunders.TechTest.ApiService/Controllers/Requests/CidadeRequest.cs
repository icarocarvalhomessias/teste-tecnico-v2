namespace Thunders.TechTest.ApiService.Controllers.Requests;

public record CidadeRequest
{
    public string Nome { get; set; }
    public Guid EstadoId { get; set; }

    public CidadeRequest(string nome, Guid estadoId)
    {
        Nome = nome;
        EstadoId = estadoId;
    }
}
