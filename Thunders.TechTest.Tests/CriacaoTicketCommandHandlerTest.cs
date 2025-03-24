using Moq;
using Thunders.TechTest.ApiService.Application.Commands;
using Thunders.TechTest.ApiService.Application.Handlers;
using Thunders.TechTest.ApiService.Data.Repositories;
using Thunders.TechTest.ApiService.Entities;

namespace Thunders.TechTest.Tests;

public class CriacaoTicketCommandHandlerTest
{
    private readonly CriacaoTicketCommandHandler _handler;
    private readonly Mock<ITicketPedagioRepository> _ticketRepository  = new Mock<ITicketPedagioRepository>();

    public CriacaoTicketCommandHandlerTest()
    {
        decimal valor = 10.20m;

        _handler = new CriacaoTicketCommandHandler(_ticketRepository.Object);

        _ticketRepository.Setup(x => x.GetPedagioById(It.IsAny<Guid>())).ReturnsAsync(new Pedagio("Pedagio 1", Guid.NewGuid(), valor));
        _ticketRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResult_WhenPedagioIdEmpy()
    {
        decimal valor = 10.20m;
        var command = new CriacaoTicketCommand(Guid.Empty, valor, 0);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("PedagioId é um campo requerido", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResult_WhenRequestIsInvalid()
    {
        decimal valor = 10.20m;

        var command = new CriacaoTicketCommand(Guid.NewGuid(), 0, 0);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        ///todo: colocar as mensagens de erro em alguma constante, para se caso alterar, alterar apenas em um lugar.
        Assert.Equal("Valor inválido", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Handle_ShouldSucceed()
    {
        decimal valor = 10.20m;

        var command = new CriacaoTicketCommand(Guid.NewGuid(), valor, 0);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResult_WhenTicketIsInvalid()
    {
        decimal valor = 10.20m;
        var command = new CriacaoTicketCommand(Guid.NewGuid(), valor, 5);
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("Tipo de veículo inválido", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResult_WhenPedagioNotFound()
    {
        decimal valor = 10.20m;
        var command = new CriacaoTicketCommand(Guid.NewGuid(), valor, 0);
        _ticketRepository.Setup(x => x.GetPedagioById(It.IsAny<Guid>())).ReturnsAsync((Pedagio?)null);
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("Pedágio não encontrado", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResult_WhenUnitOfWorkCommitFails()
    {
        decimal valor = 10.20m;
        var command = new CriacaoTicketCommand(Guid.NewGuid(), valor, 0);
        _ticketRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(false);
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("Erro ao salvar ticket", result.Errors.First().ErrorMessage);
    }
}
