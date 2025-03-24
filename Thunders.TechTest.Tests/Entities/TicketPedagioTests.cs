using Thunders.TechTest.ApiService.Entities;

namespace Thunders.TechTest.Tests.Entities;

public class TicketPedagioTests
{
    [Fact(DisplayName = "Given a valid ticket, should results is Valid ")]
    public void Given_ValidTicketData_When_Validated_Then_ShouldReturnValid()
    {
        var ticketPedagio = new TicketPedagio(Guid.NewGuid(), 10, TipoVeiculo.Carro);

        var results = ticketPedagio.Validate();

        Assert.True(results.IsValid);
    }

    [Fact(DisplayName = "Given an invalid PedagioId, should result in Invalid")]
    public void Given_InvalidPedagioId_When_Validated_Then_ShouldReturnInvalid()
    {
        var ticketPedagio = new TicketPedagio(Guid.Empty, 10, TipoVeiculo.Carro);

        var results = ticketPedagio.Validate();

        Assert.False(results.IsValid);
        Assert.Contains(results.Errors, e => e.Detail == "Pedágio é obrigatório");
    }

    [Fact(DisplayName = "Given an invalid Valor, should result in Invalid")]
    public void Given_InvalidValor_When_Validated_Then_ShouldReturnInvalid()
    {
        var ticketPedagio = new TicketPedagio(Guid.NewGuid(), -1, TipoVeiculo.Carro);

        var results = ticketPedagio.Validate();

        Assert.False(results.IsValid);
        Assert.Contains(results.Errors, e => e.Detail == "Valor é obrigatório");
    }
}
