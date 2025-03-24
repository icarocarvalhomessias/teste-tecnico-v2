using MediatR;
using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.ApiService.Application.Commands;
using Thunders.TechTest.ApiService.Controllers.Requests;

namespace Thunders.TechTest.ApiService.Controllers;

[Route("api/[controller]")]
public class PedagioController : MainController
{
    private readonly IMediator _mediator;

    public PedagioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cria um pedágio
    /// </summary>
    /// <param name="pedagioRequest"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] PedagioRequest pedagioRequest)
    {
        var command = new CriacaoPedagioCommand(pedagioRequest.Nome, pedagioRequest.Valor, pedagioRequest.CidadeId);
        var response = await _mediator.Send(command);
        if (ResponsePossuiErros(response))
        {
            return CustomResponse();
        }
        return CustomResponse(response);

    }
}
