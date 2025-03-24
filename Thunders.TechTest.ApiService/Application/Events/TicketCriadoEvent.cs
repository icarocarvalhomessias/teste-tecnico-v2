using MediatR;
using MongoDB.Driver;
using Thunders.TechTest.ApiService.Common;
using Thunders.TechTest.ApiService.Entities;

public class TicketCriadoEvent : Event
{
    public TicketPedagio Ticket { get; set; }

    public TicketCriadoEvent(TicketPedagio ticket)
    {
        Ticket = ticket;
    }
}

public class TicketCriadoEventHandler : INotificationHandler<TicketCriadoEvent>
{
    private readonly IMongoCollection<TicketDocument> _ticketsCollection;
    private readonly IMongoCollection<PracaFaturamentoMesDocument> _pracaFaturamentoMesCollection;

    public TicketCriadoEventHandler(IMongoDatabase database)
    {
        _ticketsCollection = database.GetCollection<TicketDocument>("Tickets");
        _pracaFaturamentoMesCollection = database.GetCollection<PracaFaturamentoMesDocument>("PracaFaturamentoMes");
    }

    public async Task Handle(TicketCriadoEvent notification, CancellationToken cancellationToken)
    {
        var ticket = notification.Ticket;

        var ticketDocument = new TicketDocument
        {
            CidadeId = ticket.PraçaPedagio.CidadeId,
            Valor = ticket.Valor,
            Ano = ticket.DataUtilizacao.Year,
            Mes = ticket.DataUtilizacao.Month,
            Dia = ticket.DataUtilizacao.Day,
            Hora = ticket.DataUtilizacao.Hour,
            DataHoraUtilizacao = ticket.DataUtilizacao,
            Praca = ticket.PraçaPedagio,
            PracaId = ticket.PraçaPedagio.Id,
            Cidade = ticket.PraçaPedagio.Cidade,
            Estado = ticket.PraçaPedagio.Cidade.Estado,
            TipoVeiculo = ticket.TipoVeiculo,
        };

        await _ticketsCollection.InsertOneAsync(ticketDocument, cancellationToken: cancellationToken);

        var filter = Builders<PracaFaturamentoMesDocument>.Filter.And(
            Builders<PracaFaturamentoMesDocument>.Filter.Eq("NomePraca", ticket.PraçaPedagio.Nome),
            Builders<PracaFaturamentoMesDocument>.Filter.Eq("Ano", ticket.DataUtilizacao.Year),
            Builders<PracaFaturamentoMesDocument>.Filter.Eq("Mes", ticket.DataUtilizacao.Month)
        );

        var update = Builders<PracaFaturamentoMesDocument>.Update.Inc(p => p.ValorTotal, ticket.Valor);
        var options = new UpdateOptions { IsUpsert = true };

        await _pracaFaturamentoMesCollection.UpdateOneAsync(filter, update, options, cancellationToken);
    }
}
