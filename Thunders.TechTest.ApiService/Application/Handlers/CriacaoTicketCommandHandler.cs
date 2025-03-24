using FluentValidation.Results;
using MediatR;
using Thunders.TechTest.ApiService.Application.Commands;
using Thunders.TechTest.ApiService.Data.Repositories;
using Thunders.TechTest.ApiService.Entities;

namespace Thunders.TechTest.ApiService.Application.Handlers;

public class CriacaoTicketCommandHandler : IRequestHandler<CriacaoTicketCommand, ValidationResult>
{
    private readonly IMediator _mediator;
    private readonly ITicketPedagioRepository _ticketPedagioRepository;

    public CriacaoTicketCommandHandler(ITicketPedagioRepository ticketPedagioRepository, IMediator mediator)
    {
        _ticketPedagioRepository = ticketPedagioRepository;
        _mediator = mediator;
    }

    public async Task<ValidationResult> Handle(CriacaoTicketCommand request, CancellationToken cancellationToken)
    {
        var (results, pedagio) = await Validate(request);

        if(!results.IsValid)
        {
            return results;
        }

        var ticket = new TicketPedagio(request.PedagioId, pedagio!.Valor, (TipoVeiculo)request.TipoVeiculo);
        ticket.PraçaPedagio = pedagio;

        if (request.DataUtilizacao.HasValue)
        {
            ticket.AlteradataUtilizacao(request.DataUtilizacao.Value);
        }

        _ticketPedagioRepository.Adicionar(ticket);

        if(await _ticketPedagioRepository.UnitOfWork.Commit())
        {
            _mediator.Publish(new TicketCriadoEvent(ticket), cancellationToken);

            return new ValidationResult();
        }

        return new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Ticket", "Erro ao salvar ticket") });
    }

    private async Task<(ValidationResult?, Pedagio?)> Validate(CriacaoTicketCommand request)
    {
        var results = new ValidationResult();

        if (!request.IsValid())
        {
            return (request.ValidationResult, null);
        }

        var pedagio = await _ticketPedagioRepository.GetPedagioById(request.PedagioId);

        if (pedagio is null) results.Errors.Add(new ValidationFailure("PedagioId", "Pedágio não encontrado"));

        return (results, pedagio);
    }
}
