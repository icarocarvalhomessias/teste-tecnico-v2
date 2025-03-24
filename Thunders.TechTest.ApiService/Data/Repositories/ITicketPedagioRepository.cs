using Thunders.TechTest.ApiService.Entities;

namespace Thunders.TechTest.ApiService.Data.Repositories;

//      Valor total por hora por cidade
//      As praças que mais faturaram por mês(a quantidade a ser processada deve ser configurável)
//      Quantos tipos de veículos passaram em uma determinada praça

public interface ITicketPedagioRepository : IRepository<TicketPedagio>
{
    Task<IEnumerable<TicketPedagio>> ObterTodos();
    
    void Adicionar(TicketPedagio ticketPedagio);
    void Atualizar(TicketPedagio ticketPedagio);

    void Adicionar(Pedagio pedagio);
    void Atualizar(Pedagio pedagio);

    void Adicionar(Cidade cidade);
    void Atualizar(Cidade cidade);

    void Adicionar(Estado estado);
    void Atualizar(Estado estado);

    Task<Pedagio?> GetPedagioById(Guid id);
    Task<Cidade?> GetCidadeById(Guid id);
    Task<Pedagio> GetPedagioByNome(string nome);
    Task<Estado?> GetEstadoById(Guid estadoId);
    Task<Cidade?> GetCidadeByNome(string nome);
    Task<Estado?> GetEstadoByNome(string nome);
    Task<Estado?> GetEstadoBySigla(string sigla);
    Task<List<Cidade>> GetCidades();
    Task<List<Estado>> GetEstados();
    Task InserirEstados(List<Estado> novosEstados);
    Task InserirCidades(List<Cidade> novasCidades);
    Task<List<Pedagio>> GetPedagios();
    Task InserirPegadio(List<Pedagio> novosPedagios);
}
