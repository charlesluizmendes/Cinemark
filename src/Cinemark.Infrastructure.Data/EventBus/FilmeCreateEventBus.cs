using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Models;
using Cinemark.Infrastructure.Data.Context;
using Cinemark.Infrastructure.Data.EventBus.Option;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cinemark.Infrastructure.Data.EventBus
{
    public class FilmeCreateEventBus : BaseEventBus<Filme>, IFilmeCreateEventBus
    {
        private const string queueName = "Filme_Created";

        private readonly MongoContext _mongoContext;
        private IMongoCollection<Filme> _mongoCollection;

        public FilmeCreateEventBus(IOptions<RabbitMqConfiguration> rabbitMqConfiguration,
            MongoContext mongoContext)
            : base(rabbitMqConfiguration, queueName)
        {
            _mongoContext = mongoContext;
            _mongoCollection = _mongoContext.GetCollection<Filme>(typeof(Filme).Name);
        }

        public async Task HandlerMessageAsync(Filme filme)
        {
            try
            {
                await _mongoCollection.InsertOneAsync(filme);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
