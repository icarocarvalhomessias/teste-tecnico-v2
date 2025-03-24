using MediatR;
using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.ApiService.Application.Commands;
using Thunders.TechTest.ApiService.Application.Queries;
using Thunders.TechTest.ApiService.Controllers.Requests;

namespace Thunders.TechTest.ApiService.Controllers
{
    [Route("api/[controller]")]
    public class TicketController : MainController
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria um ticket
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TicketRequest request)
        {
            var command = new CriacaoTicketCommand(request.PedagioId, (int)request.TipoVeiculo, request.DataUtilizacao);
            var response = await _mediator.Send(command);
            if (ResponsePossuiErros(response))
            {
                return CustomResponse();
            }
            return CustomResponse(response);
        }


        /// <summary>
        /// Retorna as praças que mais faturaram em um determinado mês
        /// </summary>
        /// <param name="ano"></param>
        /// <param name="mes"></param>
        /// <param name="quantidade"></param>
        /// <returns></returns>
        [HttpGet("pracas-que-mais-faturaram-por-mes")]
        public async Task<ActionResult> PracasQueMaisFaturaramPorMes([FromQuery] int ano, [FromQuery] int mes, [FromQuery] int quantidade)
        {
            var query = new PracasQueMaisFaturaramPorMesQuery(ano, mes, quantidade);
            var response = await _mediator.Send(query);
            return CustomResponse(response);
        }


        /// <summary>
        /// Retorna o valor total por hora por cidade
        /// </summary>
        /// <returns></returns>
        [HttpGet("valor-total-por-hora-por-cidade")]
        public async Task<ActionResult> ValorTotalPorHoraPorCidade()
        {
            var data = DateTime.Now;
            var query = new ValorTotalPorHoraPorCidadeQuery(data);
            var response = await _mediator.Send(query);
            return CustomResponse(response);
        }

        /// <summary>
        /// Retorna os tipos de veículos por praça
        /// </summary>
        /// <param name="pracaId"></param>
        /// <returns></returns>
        [HttpGet("tipo-de-veiculos-por-praca")]
        public async Task<ActionResult> TipoDeVeiculosPorPraca([FromQuery] Guid pracaId)
        {
            var query = new TipoDeVeiculosPorPracaQuery(pracaId);
            var response = await _mediator.Send(query);
            return CustomResponse(response);
        }
    }
}
