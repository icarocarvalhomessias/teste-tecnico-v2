using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Entities;

namespace Thunders.TechTest.ApiService.Data.Repositories;

public class TicketPedagioRepository : ITicketPedagioRepository
{

    private readonly ThundersContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public TicketPedagioRepository(ThundersContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TicketPedagio>> ObterTodos()
    {
        return await _context.Tickets.AsNoTracking().ToListAsync();
    }

    public void Adicionar(TicketPedagio ticketPedagio)
    {
        _context.Tickets.Add(ticketPedagio);
    }
    public void Atualizar(TicketPedagio ticketPedagio)
    {
        _context.Tickets.Update(ticketPedagio);
    }

    public void Adicionar(Pedagio pedagio)
    {
        _context.Pedagios.Add(pedagio);
    }

    public void Adicionar(Cidade cidade)
    {
        _context.Cidades.Add(cidade);
    }

    public void Adicionar(Estado estado)
    {
        _context.Estados.Add(estado);
    }

    public void Atualizar(Pedagio pedagio)
    {
        _context.Pedagios.Update(pedagio);
    }

    public void Atualizar(Cidade cidade)
    {
        _context.Cidades.Update(cidade);
    }

    public void Atualizar(Estado estado)
    {
        _context.Estados.Update(estado);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }

    public async Task<Pedagio?> GetPedagioById(Guid id)
    {
        return  await _context.Pedagios
            .Include(p => p.Cidade)
            .Include(p => p.Cidade.Estado)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Cidade?> GetCidadeById(Guid id)
    {
        return await _context.Cidades
            .Include(c => c.Estado)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Pedagio> GetPedagioByNome(string nome)
    {
        return await _context.Pedagios.FirstOrDefaultAsync(p => p.Nome == nome);
    }

    public async Task<Estado?> GetEstadoById(Guid estadoId)
    {
        return await _context.Estados.FirstOrDefaultAsync(e => e.Id == estadoId);
    }

    public async Task<Cidade?> GetCidadeByNome(string nome)
    {
        return await _context.Cidades.FirstOrDefaultAsync(c => c.Nome == nome);
    }

    public async Task<Estado?> GetEstadoByNome(string nome)
    {
        return await _context.Estados.FirstOrDefaultAsync(e => e.Nome == nome);
    }

    public async Task<Estado?> GetEstadoBySigla(string sigla)
    {
        return await _context.Estados.FirstOrDefaultAsync(e => e.Sigla == sigla);
    }

    public async Task<List<Cidade>> GetCidades()
    {
        return await _context.Cidades.ToListAsync();
    }

    public async Task<List<Estado>> GetEstados()
    {
        return await _context.Estados.ToListAsync();
    }

    public async Task InserirEstados(List<Estado> novosEstados)
    {
        await _context.Estados.AddRangeAsync(novosEstados);
        await _context.SaveChangesAsync();
    }

    public async Task InserirCidades(List<Cidade> novasCidades)
    {
        await _context.Cidades.AddRangeAsync(novasCidades);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Pedagio>> GetPedagios()
    {
        return await _context.Pedagios
            .Include(p => p.Cidade)
            .ThenInclude(c => c.Estado)
            .ToListAsync();
    }

    public async Task InserirPegadio(List<Pedagio> novosPedagios)
    {
        await _context.Pedagios.AddRangeAsync(novosPedagios);
        await _context.SaveChangesAsync();
    }
}
