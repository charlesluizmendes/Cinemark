using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Models;
using Cinemark.Infrastructure.Data.Context;
using Cinemark.Infrastructure.Data.EventBus.Option;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cinemark.Infrastructure.Data.EventBus
{
    public class FilmeEventBus : BaseEventBus<Filme>, IFilmeEventBus
    {
        private readonly MongoContext _mongoContext;
        private IMongoCollection<Filme> _mongoCollection;

        public FilmeEventBus(IOptions<RabbitMqConfiguration> rabbitMqConfiguration,
            MongoContext mongoContext)
            : base(rabbitMqConfiguration)
        {
            _mongoContext = mongoContext;
            _mongoCollection = _mongoContext.GetCollection<Filme>(typeof(Filme).Name);
        }

        public async Task<bool> HandlerInsertAsync(Filme filme)
        {
            try
            {
                await _mongoCollection.InsertOneAsync(filme);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> HandlerUpdateAsync(Filme filme)
        {
            try
            {
                await _mongoCollection.ReplaceOneAsync(Builders<Filme>.Filter.Eq("_id", filme.Id), filme);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> HandlerDeleteAsync(Filme filme)
        {
            try
            {
                await _mongoCollection.DeleteOneAsync(Builders<Filme>.Filter.Eq("_id", filme.Id));

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
