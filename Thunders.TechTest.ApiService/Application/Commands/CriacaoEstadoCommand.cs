using FluentValidation;
using Thunders.TechTest.ApiService.Common;

namespace Thunders.TechTest.ApiService.Application.Commands;

public class CriacaoEstadoCommand : Command
{
    public string Nome { get; set; }
    public string Sigla { get; set; }

    public CriacaoEstadoCommand(string nome, string sigla)
    {
        Nome = nome;
        Sigla = sigla;
    }

    public override bool IsValid()
    {
        ValidationResult = new CriacaoEstadoCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class CriacaoEstadoCommandValidation : AbstractValidator<CriacaoEstadoCommand>
{
    public CriacaoEstadoCommandValidation()
    {
        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("Nome é um campo requerido")
            .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres.");
        RuleFor(c => c.Sigla)
            .NotEmpty().WithMessage("Sigla é um campo requerido")
            .MaximumLength(2).WithMessage("Sigla deve ter no máximo 2 caracteres.");
    }
}
