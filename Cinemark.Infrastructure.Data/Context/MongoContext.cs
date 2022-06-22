using Cinemark.Infrastructure.Data.Context.Option;
using Cinemark.Infrastructure.Data.Mapping.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cinemark.Infrastructure.Data.Context
{
    public class MongoContext
    {
        private IMongoDatabase _databaseName { get; set; }
        private MongoClient _mongoClient { get; set; }

        public MongoContext(IOptions<MongoConfiguration> configuration)
        {
            _databaseName = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
            _mongoClient = new MongoClient(configuration.Value.Connection);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _databaseName.GetCollection<T>(name);
        }

        public static void OnModelCreating()
        {
            FilmeMap.Configure();
            UsuarioMap.Configure();
        }
    }
}
