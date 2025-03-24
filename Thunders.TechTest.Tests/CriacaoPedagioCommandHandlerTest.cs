using Moq;
using Thunders.TechTest.ApiService.Application.Commands;
using Thunders.TechTest.ApiService.Application.Handlers;
using Thunders.TechTest.ApiService.Data.Repositories;
using Thunders.TechTest.ApiService.Entities;

namespace Thunders.TechTest.Tests;

public class CriacaoPedagioCommandHandlerTest
{
    private readonly CriacaoPedagioCommandHandler _handler;
    private readonly Mock<ITicketPedagioRepository> _ticketPedagioRepository = new Mock<ITicketPedagioRepository>();
    public CriacaoPedagioCommandHandlerTest()
    {
        _handler = new CriacaoPedagioCommandHandler(_ticketPedagioRepository.Object);

        _ticketPedagioRepository.Setup(x => x.GetCidadeById(It.IsAny<Guid>())).ReturnsAsync(new Cidade("Cidade 1", Guid.NewGuid()));
        _ticketPedagioRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
    }

    [Fact]
    public async Task Handle_ShouldSucceed()
    {
        var command = new CriacaoPedagioCommand("Pedagio 1", 10.1m, Guid.NewGuid());
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResult_WhenCidadeNotFound()
    {
        var command = new CriacaoPedagioCommand("Pedagio 1", 10.1m, Guid.NewGuid());
        _ticketPedagioRepository.Setup(x => x.GetCidadeById(It.IsAny<Guid>())).ReturnsAsync((Cidade)null);
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("Cidade não encontrada", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResult_WhenPedagioNameIsInvalid()
    {
        var command = new CriacaoPedagioCommand("", 0, Guid.NewGuid());
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        Assert.False(result.IsValid);
        Assert.Equal(2, result.Errors.Count);
        Assert.Equal("Nome é um campo requerido", result.Errors.First().ErrorMessage);
        Assert.Equal("Valor inválido", result.Errors.Last().ErrorMessage);
    }


}