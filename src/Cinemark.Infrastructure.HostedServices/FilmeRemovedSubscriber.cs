using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Cinemark.Infrastructure.Data.Context;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace Cinemark.Infrastructure.HostedServices
{
    public class FilmeRemovedSubscriber : BackgroundService
    {
        private readonly IFilmeEventBus _filmeEventBus;
        private readonly MongoContext _mongoContext;

        public FilmeRemovedSubscriber(IFilmeEventBus filmeEventBus, MongoContext mongoContext)
        {
            _filmeEventBus = filmeEventBus ?? throw new ArgumentNullException(nameof(filmeEventBus));
            _mongoContext = mongoContext ?? throw new ArgumentNullException(nameof(mongoContext));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queueName = typeof(Filme).Name + FilmeQueue.Removed.Name;

            _filmeEventBus.CreateQueue(queueName);
            await _filmeEventBus.SubscriberAsync(queueName, async (filme, stoppingToken) =>
                await DeleteFilmeAsync(filme)
            );
        }        

        private async Task<bool> DeleteFilmeAsync(Filme filme)
        {
            await _mongoContext.GetCollection<Filme>().DeleteOneAsync(Builders<Filme>.Filter.Eq("_id", filme.Id.ToString()));

            return true;
        }
    }
}
