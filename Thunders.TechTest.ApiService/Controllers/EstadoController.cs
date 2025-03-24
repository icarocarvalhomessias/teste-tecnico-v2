using MediatR;
using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.ApiService.Application.Commands;
using Thunders.TechTest.ApiService.Controllers.Requests;

namespace Thunders.TechTest.ApiService.Controllers;

[Route("api/[controller]")]

public class EstadoController : MainController
{

    private readonly IMediator _mediator;

    public EstadoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cria um estado
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] EstadoRequest request)
    {
        var command = new CriacaoEstadoCommand(request.Nome, request.Sigla);
        var response = await _mediator.Send(command);
        if (ResponsePossuiErros(response))
        {
            return CustomResponse();
        }
        return CustomResponse(response);
    }

}
