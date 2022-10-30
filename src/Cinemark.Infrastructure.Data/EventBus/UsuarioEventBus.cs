using System;
using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Models;
using Cinemark.Infrastructure.Data.Context;
using Cinemark.Infrastructure.Data.EventBus.Option;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cinemark.Infrastructure.Data.EventBus
{
    public class UsuarioEventBus : BaseEventBus<Usuario>, IUsuarioEventBus
    {
        private readonly MongoContext _mongoContext;
        private IMongoCollection<Usuario> _mongoCollection;

        public UsuarioEventBus(IOptions<RabbitMqConfiguration> rabbitMqConfiguration,
            MongoContext mongoContext)
            : base(rabbitMqConfiguration)
        {
            _mongoContext = mongoContext;
            _mongoCollection = _mongoContext.GetCollection<Usuario>(typeof(Usuario).Name);
        }

        public async Task<bool> HandlerInsertAsync(Usuario usuario)
        {
            try
            {
                await _mongoCollection.InsertOneAsync(usuario);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> HandlerUpdateAsync(Usuario usuario)
        {
            try
            {
                await _mongoCollection.ReplaceOneAsync(Builders<Usuario>.Filter.Eq("_id", usuario.Id), usuario);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> HandlerDeleteAsync(Usuario usuario)
        {
            try
            {
                await _mongoCollection.DeleteOneAsync(Builders<Usuario>.Filter.Eq("_id", usuario.Id));

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
