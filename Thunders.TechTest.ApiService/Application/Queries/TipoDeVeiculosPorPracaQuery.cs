using MediatR;
using MongoDB.Driver;
using Thunders.TechTest.ApiService.Entities;
using Thunders.TechTest.ApiService.Models;

namespace Thunders.TechTest.ApiService.Application.Queries;

public class TipoDeVeiculosPorPracaQuery : IRequest<TipoDeVeiculosPorPracaViewModel>
{
    public Guid PracaId { get; set; }

    public TipoDeVeiculosPorPracaQuery(Guid pracaId)
    {
        PracaId = pracaId;
    }
}

public class TipoDeVeiculosPorPracaQueryHandler : IRequestHandler<TipoDeVeiculosPorPracaQuery, TipoDeVeiculosPorPracaViewModel>
{
    private readonly IMongoCollection<TicketDocument> _ticketsCollection;

    public TipoDeVeiculosPorPracaQueryHandler(IMongoDatabase database)
    {
        _ticketsCollection = database.GetCollection<TicketDocument>("Tickets");
    }

    public async Task<TipoDeVeiculosPorPracaViewModel> Handle(TipoDeVeiculosPorPracaQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<TicketDocument>.Filter.Eq("PracaId", request.PracaId);
        var result = await _ticketsCollection.Find(filter).ToListAsync(cancellationToken);

        var tiposDeVeiculos = result.GroupBy(x => x.TipoVeiculo)
            .Select(g => g.Key.ToString())
            .Distinct()
            .ToList();

        var nomePraca = result.FirstOrDefault()?.Praca.Nome ?? "Praca não encontrada";

        return new TipoDeVeiculosPorPracaViewModel
        {
            NomePraca = nomePraca,
            TiposDeVeiculos = tiposDeVeiculos
        };
    }
}

