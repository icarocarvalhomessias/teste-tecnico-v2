namespace Thunders.TechTest.ApiService.Controllers.Requests
{
    public record TicketRequest
    {
        public Guid PedagioId { get; }
        public TicketTipoVeiculo TipoVeiculo { get; }
        public DateTime? DataUtilizacao { get; }

        public TicketRequest(Guid pedagioId, TicketTipoVeiculo tipoVeiculo, DateTime? dataUtilizacao)
        {
            PedagioId = pedagioId;
            TipoVeiculo = tipoVeiculo;
            DataUtilizacao = dataUtilizacao;
        }
    }

    public enum TicketTipoVeiculo
    {
        Carro,
        Moto,
        Caminhao
    }
}
