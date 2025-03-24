using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Thunders.TechTest.ApiService.Entities;

public class PracaFaturamentoMesDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string NomePraca { get; set; }
    public decimal ValorTotal { get; set; }
    public int Ano { get; set; }
    public int Mes { get; set; }
}
