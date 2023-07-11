using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Cinemark.Infrastructure.Data.Context;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace Cinemark.Infrastructure.HostedServices
{
    public class FilmeUpdatedSubscriber : BackgroundService
    {
        private readonly IFilmeEventBus _filmeEventBus;
        private readonly MongoContext _mongoContext;

        public FilmeUpdatedSubscriber(IFilmeEventBus filmeEventBus, MongoContext mongoContext)
        {
            _filmeEventBus = filmeEventBus ?? throw new ArgumentNullException(nameof(filmeEventBus));
            _mongoContext = mongoContext ?? throw new ArgumentNullException(nameof(mongoContext));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queueName = typeof(Filme).Name + FilmeQueue.Updated.Name;

            _filmeEventBus.CreateQueue(queueName);
            await _filmeEventBus.SubscriberAsync(queueName, async (filme, stoppingToken) =>
                await UpdateFilmeAsync(filme)
            );
        }

        private async Task<bool> UpdateFilmeAsync(Filme filme)
        {
            await _mongoContext.GetCollection<Filme>().ReplaceOneAsync(Builders<Filme>.Filter.Eq("_id", filme.Id.ToString()), filme);

            return true;
        }
    }
}
