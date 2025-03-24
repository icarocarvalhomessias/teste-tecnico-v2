using MongoDB.Driver;
using Thunders.TechTest.ApiService.Entities;

namespace Thunders.TechTest.ApiService.Data.MongoDb
{
    public class MongoDbIndexConfigurator
    {
        private readonly IMongoDatabase _database;

        public MongoDbIndexConfigurator(IMongoDatabase database)
        {
            _database = database;
        }

        public void ConfigureIndexes()
        {
            var ticketsCollection = _database.GetCollection<TicketDocument>("Tickets");
            var pracaFaturamentoMesCollection = _database.GetCollection<PracaFaturamentoMesDocument>("PracaFaturamentoMes");

            var ticketIndexKeys = Builders<TicketDocument>.IndexKeys
                .Ascending(t => t.CidadeId)
                .Ascending(t => t.DataHoraUtilizacao)
                .Ascending(t => t.TipoVeiculoId);
            ticketsCollection.Indexes.CreateOne(new CreateIndexModel<TicketDocument>(ticketIndexKeys));

            var pracaFaturamentoMesIndexKeys = Builders<PracaFaturamentoMesDocument>.IndexKeys
                .Ascending(p => p.Ano)
                .Ascending(p => p.Mes)
                .Ascending(p => p.NomePraca);
            pracaFaturamentoMesCollection.Indexes.CreateOne(new CreateIndexModel<PracaFaturamentoMesDocument>(pracaFaturamentoMesIndexKeys));

            var indexKeysDefinition = Builders<TicketDocument>.IndexKeys.Ascending(t => t.PracaId);
            var indexModel = new CreateIndexModel<TicketDocument>(indexKeysDefinition);
            ticketsCollection.Indexes.CreateOne(indexModel);
        }
    }
}
