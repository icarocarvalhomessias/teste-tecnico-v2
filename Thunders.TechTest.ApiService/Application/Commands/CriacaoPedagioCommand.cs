using FluentValidation;
using Thunders.TechTest.ApiService.Common;

namespace Thunders.TechTest.ApiService.Application.Commands;

public class CriacaoPedagioCommand : Command
{
    public string Nome { get; set; }
    public decimal Valor { get; set; }
    public Guid CidadeId { get; set; }

    public CriacaoPedagioCommand(string nome, decimal valor, Guid cidadeId)
    {
        Nome = nome;
        Valor = valor;
        CidadeId = cidadeId;
    }
    public override bool IsValid()
    {
        ValidationResult = new CriacaoPedagioCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class CriacaoPedagioCommandValidation : AbstractValidator<CriacaoPedagioCommand>
{
    public CriacaoPedagioCommandValidation()
    {
        RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage("Nome é um campo requerido");
        RuleFor(c => c.Valor)
            .GreaterThan(0)
            .WithMessage("Valor inválido");
    }
}
