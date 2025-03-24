using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Thunders.TechTest.ApiService.Entities;

public class TicketDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public Guid CidadeId { get; set; }
    public decimal Valor { get; set; }
    public int Ano { get; set; }
    public int Mes { get; set; }
    public int Dia { get; set; }
    public int Hora { get; set; }
    public Guid PracaId { get; set; }
    public DateTime DataHoraUtilizacao { get; set; }
    public Pedagio Praca { get; set; }
    public Cidade Cidade { get; set; }
    public Estado Estado { get; set; }
    public TipoVeiculo TipoVeiculo { get; set; }
    public int TipoVeiculoId { get; set; }
}
