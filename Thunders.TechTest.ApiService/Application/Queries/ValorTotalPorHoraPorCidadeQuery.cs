using MediatR;
using MongoDB.Driver;
using Thunders.TechTest.ApiService.Common;
using Thunders.TechTest.ApiService.Entities;
using Thunders.TechTest.ApiService.Models;

namespace Thunders.TechTest.ApiService.Application.Queries;

public class ValorTotalPorHoraPorCidadeQuery : IRequest<List<ValorTotalPorHoraPorCidadeViewModel>>
{
    public DateTime Data { get; set; }

    public ValorTotalPorHoraPorCidadeQuery(DateTime data)
    {
        Data = data;
    }
}

public class ValorTotalPorHoraPorCidadeQueryHandler : IRequestHandler<ValorTotalPorHoraPorCidadeQuery, List<ValorTotalPorHoraPorCidadeViewModel>>
{
    private readonly IMongoCollection<TicketDocument> _ticketsCollection;

    public ValorTotalPorHoraPorCidadeQueryHandler(IMongoDatabase database)
    {
        _ticketsCollection = database.GetCollection<TicketDocument>("Tickets");
    }

    public async Task<List<ValorTotalPorHoraPorCidadeViewModel>> Handle(ValorTotalPorHoraPorCidadeQuery request, CancellationToken cancellationToken)
    {
        var startDate = request.Data.Date;
        var endDate = startDate.AddDays(1);

        var filter = Builders<TicketDocument>.Filter.And(
            Builders<TicketDocument>.Filter.Gte("DataHoraUtilizacao", startDate),
            Builders<TicketDocument>.Filter.Lt("DataHoraUtilizacao", endDate)
        );

        var result = await _ticketsCollection.Find(filter).ToListAsync(cancellationToken);

        var retorno = result.GroupBy(x => new { x.CidadeId, x.DataHoraUtilizacao.Hour })
            .Select(g => new ValorTotalPorHoraPorCidadeViewModel
            {
                Horas = HoursHelper.FormatHour(g.Key.Hour),
                Cidade = g.FirstOrDefault()?.Cidade.Nome,
                ValorTotal = g.Sum(y => y.Valor)
            })
            .OrderBy(x => x.Cidade).ThenBy(x => x.Horas)
            .ToList();

        return retorno;
    }


}
