using Moq;
using Thunders.TechTest.ApiService.Application.Commands;
using Thunders.TechTest.ApiService.Application.Handlers;
using Thunders.TechTest.ApiService.Data.Repositories;
using Thunders.TechTest.ApiService.Entities;
using FluentValidation.Results;
using Xunit;

namespace Thunders.TechTest.Tests;

public class CriacaoEstadoCommandHandlerTest
{
    private readonly CriacaoEstadoCommandHandler _handler;
    private readonly Mock<ITicketPedagioRepository> _ticketRepository = new Mock<ITicketPedagioRepository>();

    public CriacaoEstadoCommandHandlerTest()
    {
        _handler = new CriacaoEstadoCommandHandler(_ticketRepository.Object);

        _ticketRepository.Setup(x => x.GetEstadoByNome(It.IsAny<string>())).ReturnsAsync((Estado)null);
        _ticketRepository.Setup(x => x.GetEstadoBySigla(It.IsAny<string>())).ReturnsAsync((Estado)null);
        _ticketRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResult_WhenNomeIsEmpty()
    {
        var command = new CriacaoEstadoCommand("", "E1");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("Nome é um campo requerido", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResult_WhenSiglaIsEmpty()
    {
        var command = new CriacaoEstadoCommand("Estado 1", "");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("Sigla é um campo requerido", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResult_WhenEstadoAlreadyExistsByNome()
    {
        var command = new CriacaoEstadoCommand("Estado 1", "E1");
        _ticketRepository.Setup(x => x.GetEstadoByNome(It.IsAny<string>())).ReturnsAsync(new Estado("Estado 1", "E1"));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("Nome já cadastrado", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResult_WhenEstadoAlreadyExistsBySigla()
    {
        var command = new CriacaoEstadoCommand("Estado 1", "E1");
        _ticketRepository.Setup(x => x.GetEstadoBySigla(It.IsAny<string>())).ReturnsAsync(new Estado("Estado 1", "E1"));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("Sigla já cadastrada", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Handle_ShouldSucceed()
    {
        var command = new CriacaoEstadoCommand("Estado 1", "E1");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResult_WhenUnitOfWorkCommitFails()
    {
        var command = new CriacaoEstadoCommand("Estado 1", "E1");
        _ticketRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("Erro ao salvar estado", result.Errors.First().ErrorMessage);
    }
}
