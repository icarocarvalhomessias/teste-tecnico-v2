using MediatR;
using MongoDB.Driver;
using Thunders.TechTest.ApiService.Entities;
using Thunders.TechTest.ApiService.Models;

public class PracasQueMaisFaturaramPorMesQuery : IRequest<List<PracasQueMaisFaturaramPorMesViewModel>>
{
    public int Ano { get; set; }
    public int Mes { get; set; }
    public int Quantidade { get; set; }

    public PracasQueMaisFaturaramPorMesQuery(int ano, int mes, int quantidade)
    {
        Ano = ano;
        Mes = mes;
        Quantidade = quantidade;
    }
}

public class PracasQueMaisFaturaramPorMesQueryHandler : IRequestHandler<PracasQueMaisFaturaramPorMesQuery, List<PracasQueMaisFaturaramPorMesViewModel>>
{
    private readonly IMongoCollection<PracaFaturamentoMesDocument> _pracaFaturamentoMesCollection;

    public PracasQueMaisFaturaramPorMesQueryHandler(IMongoDatabase database)
    {
        _pracaFaturamentoMesCollection = database.GetCollection<PracaFaturamentoMesDocument>("PracaFaturamentoMes");
    }

    public async Task<List<PracasQueMaisFaturaramPorMesViewModel>> Handle(PracasQueMaisFaturaramPorMesQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<PracaFaturamentoMesDocument>.Filter.And(
            Builders<PracaFaturamentoMesDocument>.Filter.Eq(p => p.Ano, request.Ano),
            Builders<PracaFaturamentoMesDocument>.Filter.Eq(p => p.Mes, request.Mes)
        );

        var result = await _pracaFaturamentoMesCollection.Find(filter)
            .Sort(Builders<PracaFaturamentoMesDocument>.Sort.Descending(p => p.ValorTotal))
            .Limit(request.Quantidade)
            .ToListAsync(cancellationToken);

        var pracasQueMaisFaturaramPorMes = result.Select(x => new PracasQueMaisFaturaramPorMesViewModel
        {
            NomePraca = x.NomePraca,
            ValorTotal = x.ValorTotal,
            Mes = x.Mes
        }).ToList();

        return pracasQueMaisFaturaramPorMes;
    }
}
