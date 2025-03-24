namespace Thunders.TechTest.ApiService.Controllers.Requests;

public record EstadoRequest
{
    public string Nome { get; set; }
    public string Sigla { get; set; }
    public EstadoRequest(string nome, string sigla)
    {
        Nome = nome;
        Sigla = sigla;
    }
}
