using FluentValidation;
using Thunders.TechTest.ApiService.Common;

namespace Thunders.TechTest.ApiService.Application.Commands;

public class CriacaoCidadeCommand : Command
{
    public string Nome { get; set; }
    public Guid EstadoId { get; set; }
    public CriacaoCidadeCommand(string nome, Guid estadoId)
    {
        Nome = nome;
        EstadoId = estadoId;
    }
    public override bool IsValid()
    {
        ValidationResult = new CriacaoCidadeCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class CriacaoCidadeCommandValidation : AbstractValidator<CriacaoCidadeCommand>
{
    public CriacaoCidadeCommandValidation()
    {
        RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage("Nome é um campo requerido");

        RuleFor(c => c.EstadoId)
            .NotEmpty()
            .WithMessage("EstadoId é um campo requerido");
    }
}