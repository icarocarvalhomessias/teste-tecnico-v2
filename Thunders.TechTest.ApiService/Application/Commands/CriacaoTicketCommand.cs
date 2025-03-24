using FluentValidation;
using Thunders.TechTest.ApiService.Common;
using Thunders.TechTest.ApiService.Entities;

namespace Thunders.TechTest.ApiService.Application.Commands;

public class CriacaoTicketCommand : Command
{
    public Guid PedagioId { get; set; }
    public int TipoVeiculo { get; set; }
    public DateTime? DataUtilizacao { get; set; }

    public CriacaoTicketCommand(Guid pedagioId, int tipoVeiculo, DateTime? dataUtilizacao)
    {
        PedagioId = pedagioId;
        TipoVeiculo = tipoVeiculo;
        DataUtilizacao = dataUtilizacao;
    }
    public override bool IsValid()
    {
        ValidationResult = new CriacaoTicketCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class CriacaoTicketCommandValidation : AbstractValidator<CriacaoTicketCommand>
{
    public CriacaoTicketCommandValidation()
    {
        RuleFor(c => c.PedagioId)
            .NotEqual(Guid.Empty)
            .WithMessage("PedagioId é um campo requerido");
        RuleFor(c => c.TipoVeiculo)
            .Must(BeAValidTipoVeiculo)
            .WithMessage("Tipo de veículo inválido");
    }

    private bool BeAValidTipoVeiculo(int tipoVeiculo)
    {
        return Enum.IsDefined(typeof(TipoVeiculo), tipoVeiculo);
    }
}
