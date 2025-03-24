using Moq;
using Thunders.TechTest.ApiService.Application.Commands;
using Thunders.TechTest.ApiService.Application.Handlers;
using Thunders.TechTest.ApiService.Data.Repositories;
using Thunders.TechTest.ApiService.Entities;

namespace Thunders.TechTest.Tests;

public class CriacaoCidadeCommandHandlerTest
{
    private readonly CriacaoCidadeCommandHandler _handler;
    private readonly Mock<ITicketPedagioRepository> _ticketRepository = new Mock<ITicketPedagioRepository>();

    public CriacaoCidadeCommandHandlerTest()
    {
        _handler = new CriacaoCidadeCommandHandler(_ticketRepository.Object);

        _ticketRepository.Setup(x => x.GetEstadoById(It.IsAny<Guid>())).ReturnsAsync(new Estado("Estado 1", "E1"));
        _ticketRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResult_WhenEstadoIdIsEmpty()
    {
        var command = new CriacaoCidadeCommand("Cidade 1", Guid.Empty);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("EstadoId é um campo requerido", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResult_WhenRequestIsInvalid()
    {
        var command = new CriacaoCidadeCommand("", Guid.NewGuid());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("Nome é um campo requerido", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResult_WhenEstadoNotFound()
    {
        var command = new CriacaoCidadeCommand("Cidade 1", Guid.NewGuid());
        _ticketRepository.Setup(x => x.GetEstadoById(It.IsAny<Guid>())).ReturnsAsync((Estado)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("Estado não encontrado", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResult_WhenCidadeAlreadyExists()
    {
        var command = new CriacaoCidadeCommand("Cidade 1", Guid.NewGuid());
        _ticketRepository.Setup(x => x.GetCidadeByNome(It.IsAny<string>())).ReturnsAsync(new Cidade("Cidade 1", Guid.NewGuid()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("Nome já cadastrado", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Handle_ShouldSucceed()
    {
        var command = new CriacaoCidadeCommand("Cidade 1", Guid.NewGuid());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResult_WhenUnitOfWorkCommitFails()
    {
        var command = new CriacaoCidadeCommand("Cidade 1", Guid.NewGuid());
        _ticketRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("Erro ao salvar cidade", result.Errors.First().ErrorMessage);
    }
}
