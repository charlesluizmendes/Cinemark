using Cinemark.Infrastructure.Data.Context.Option;
using Cinemark.Infrastructure.Data.Mapping;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cinemark.Infrastructure.Data.Context
{
    public class CinemarkMongoContext
    {
        private IMongoDatabase DatabaseName { get; set; }
        private MongoClient MongoClient { get; set; }

        public CinemarkMongoContext(IOptions<MongoSettings> configuration)
        {
            DatabaseName = MongoClient.GetDatabase(configuration.Value.DatabaseName);
            MongoClient = new MongoClient(configuration.Value.Connection);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return DatabaseName.GetCollection<T>(name);
        }

        public static void OnModelCreating()
        {
            FilmeMongoMap.Configure();
        }
    }
}
