using FluentValidation;

namespace Thunders.TechTest.ApiService.Entities.Validators
{
    public class PedagioValidator : AbstractValidator<Pedagio>
    {
        public PedagioValidator()
        {
            RuleFor(o => o.Nome)
                .NotEmpty()
                .WithErrorCode("NomeVazio")
                .WithMessage("Nome não pode ser vazio");
            RuleFor(o => o.CidadeId)
                .NotEmpty()
                .WithErrorCode("CidadeIdVazio")
                .WithMessage("CidadeId não pode ser vazio");
        }
    }
}
