using FluentValidation.Results;
using MediatR;
using Thunders.TechTest.ApiService.Application.Commands;
using Thunders.TechTest.ApiService.Data.Repositories;
using Thunders.TechTest.ApiService.Entities;

namespace Thunders.TechTest.ApiService.Application.Handlers;

public class CriacaoPedagioCommandHandler : IRequestHandler<CriacaoPedagioCommand, ValidationResult>
{
    private readonly ITicketPedagioRepository _ticketPedagioRepository;

    public CriacaoPedagioCommandHandler(ITicketPedagioRepository ticketPedagioRepository)
    {
        _ticketPedagioRepository = ticketPedagioRepository;
    }

    public async Task<ValidationResult> Handle(CriacaoPedagioCommand request, CancellationToken cancellationToken)
    {
        var (validationResult, cidade) = await ValidateRequest(request);
        if (validationResult != null)
        {
            return validationResult;
        }

        var pedagio = new Pedagio(request.Nome, request.CidadeId, request.Valor);

        _ticketPedagioRepository.Adicionar(pedagio);

        if (await _ticketPedagioRepository.UnitOfWork.Commit())
        {
            return new ValidationResult();
        }

        return new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Pedagio", "Erro ao salvar pedágio") });
    }

    private async Task<(ValidationResult?, Cidade?)> ValidateRequest(CriacaoPedagioCommand request)
    {
        if (!request.IsValid())
        {
            return (request.ValidationResult, null);
        }

        var cidade = await _ticketPedagioRepository.GetCidadeById(request.CidadeId);
        if (cidade == null)
        {
            return (new ValidationResult(new List<ValidationFailure> { new ValidationFailure("CidadeId", "Cidade não encontrada") }), null);
        }

        if (await _ticketPedagioRepository.GetPedagioByNome(request.Nome) != null)
        {
            return (new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Nome", "Nome já cadastrado") }), null);
        }

        return (null, cidade);
    }
}
