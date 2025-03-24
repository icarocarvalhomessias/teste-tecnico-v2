using FluentValidation;

namespace Thunders.TechTest.ApiService.Entities.Validators;

public class TicketPedagioValidator : AbstractValidator<TicketPedagio>
{
    public TicketPedagioValidator()
    {
        RuleFor(o => o.PedagioId).NotEmpty().WithMessage("Pedágio é obrigatório");
        RuleFor(o => o.Valor).GreaterThan(0).WithMessage("Valor é obrigatório");
        RuleFor(o => o.TipoVeiculo).IsInEnum().WithMessage("Tipo de veículo é obrigatório");
    }
}
