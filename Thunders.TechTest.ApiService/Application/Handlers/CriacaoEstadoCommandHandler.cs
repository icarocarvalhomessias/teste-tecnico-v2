using FluentValidation.Results;
using MediatR;
using Thunders.TechTest.ApiService.Application.Commands;
using Thunders.TechTest.ApiService.Data.Repositories;
using Thunders.TechTest.ApiService.Entities;

namespace Thunders.TechTest.ApiService.Application.Handlers;

public class CriacaoEstadoCommandHandler : IRequestHandler<CriacaoEstadoCommand, ValidationResult>
{
    private readonly ITicketPedagioRepository _ticketPedagioRepository;

    public CriacaoEstadoCommandHandler(ITicketPedagioRepository ticketPedagioRepository)
    {
        _ticketPedagioRepository = ticketPedagioRepository;
    }

    public async Task<ValidationResult> Handle(CriacaoEstadoCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await ValidateRequest(request);
        if (validationResult != null)
        {
            return validationResult;
        }

        var estado = new Estado(request.Nome, request.Sigla);

        _ticketPedagioRepository.Adicionar(estado);

        if (await _ticketPedagioRepository.UnitOfWork.Commit())
        {
            return new ValidationResult();
        }

        return new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Estado", "Erro ao salvar estado") });
    }

    private async Task<ValidationResult?> ValidateRequest(CriacaoEstadoCommand request)
    {
        if (!request.IsValid())
        {
            return request.ValidationResult;
        }

        if (await _ticketPedagioRepository.GetEstadoByNome(request.Nome) != null)
        {
            return new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Nome", "Nome já cadastrado") });
        }

        if (await _ticketPedagioRepository.GetEstadoBySigla(request.Sigla) != null)
        {
            return new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Sigla", "Sigla já cadastrada") });
        }

        return null;
    }
}
