using FluentValidation.Results;
using MediatR;
using Thunders.TechTest.ApiService.Application.Commands;
using Thunders.TechTest.ApiService.Data.Repositories;
using Thunders.TechTest.ApiService.Entities;

namespace Thunders.TechTest.ApiService.Application.Handlers;

public class CriacaoCidadeCommandHandler : IRequestHandler<CriacaoCidadeCommand, ValidationResult>
{
    private readonly ITicketPedagioRepository _ticketPedagioRepository;

    public CriacaoCidadeCommandHandler(ITicketPedagioRepository ticketPedagioRepository)
    {
        _ticketPedagioRepository = ticketPedagioRepository;
    }

    public async Task<ValidationResult> Handle(CriacaoCidadeCommand request, CancellationToken cancellationToken)
    {
        var (validationResult, estado) = await ValidateRequest(request);
        if (validationResult != null)
        {
            return validationResult;
        }

        var cidade = new Cidade(request.Nome, estado!.Id);

        _ticketPedagioRepository.Adicionar(cidade);

        if (await _ticketPedagioRepository.UnitOfWork.Commit())
        {
            return new ValidationResult();
        }

        return new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Cidade", "Erro ao salvar cidade") });
    }

    private async Task<(ValidationResult?, Estado?)> ValidateRequest(CriacaoCidadeCommand request)
    {
        if (!request.IsValid())
        {
            return (request.ValidationResult, null);
        }

        var estado = await _ticketPedagioRepository.GetEstadoById(request.EstadoId);
        if (estado == null)
        {
            return (new ValidationResult(new List<ValidationFailure> { new ValidationFailure("EstadoId", "Estado não encontrado") }), null);
        }

        if (await _ticketPedagioRepository.GetCidadeByNome(request.Nome) != null)
        {
            return (new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Nome", "Nome já cadastrado") }), null);
        }

        return (null, estado);
    }
}
