namespace Thunders.TechTest.ApiService.Entities;

public class Estado : Entity
{
    public string Nome { get; private set; }
    public string Sigla { get; private set; }

    protected Estado() { }

    public Estado(string nome, string sigla)
    {
        Nome = nome;
        Sigla = sigla;
    }
}
