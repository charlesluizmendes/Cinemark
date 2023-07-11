using Cinemark.Infrastructure.Data.Context.Option;
using Cinemark.Infrastructure.Data.Mapping.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cinemark.Infrastructure.Data.Context
{
    public class MongoContext
    {
        private MongoClient _mongoClient { get; set; }
        private IMongoDatabase _databaseName { get; set; }

        public MongoContext(IOptions<MongoConfiguration> configuration)
        {
            _mongoClient = new MongoClient(configuration.Value.Connection);
            _databaseName = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _databaseName.GetCollection<T>(typeof(T).Name);
        }

        public static void OnModelCreating()
        {
            EntityMap.Configure();
            FilmeMap.Configure();
            UsuarioMap.Configure();
        }
    }
}
