namespace Thunders.TechTest.ApiService.Entities;

public class Cidade : Entity
{
    public string Nome { get; private set; }
    public Guid EstadoId { get; private set; }
    public Estado Estado { get; private set; }

    protected Cidade() { }

    public Cidade(string nome, Guid estadoId)
    {
        Nome = nome;
        EstadoId = estadoId;
    }
}
