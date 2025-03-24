using MediatR;
using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.ApiService.Application.Commands;
using Thunders.TechTest.ApiService.Controllers.Requests;

namespace Thunders.TechTest.ApiService.Controllers
{
    [Route("api/[controller]")]

    public class CidadeController : MainController
    {
        private readonly IMediator _mediator;

        public CidadeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria uma cidade
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CidadeRequest request)
        {
            var command = new CriacaoCidadeCommand(request.Nome, request.EstadoId);
            var response = await _mediator.Send(command);
            if (ResponsePossuiErros(response))
            {
                return CustomResponse();
            }
            return CustomResponse(response);
        }

    }
}
