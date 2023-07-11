using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Cinemark.Infrastructure.Data.Context;
using Microsoft.Extensions.Hosting;

namespace Cinemark.Infrastructure.HostedServices
{
    public class FilmeCreatedSubscriber : BackgroundService
    {
        private readonly IFilmeEventBus _filmeEventBus;
        private readonly MongoContext _mongoContext;

        public FilmeCreatedSubscriber(IFilmeEventBus filmeEventBus, MongoContext mongoContext)
        {
            _filmeEventBus = filmeEventBus ?? throw new ArgumentNullException(nameof(filmeEventBus));
            _mongoContext = mongoContext ?? throw new ArgumentNullException(nameof(mongoContext));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queueName = typeof(Filme).Name + FilmeQueue.Created.Name;

            _filmeEventBus.CreateQueue(queueName);
            await _filmeEventBus.SubscriberAsync(queueName, async (filme, stoppingToken) =>
                await InsertFilmeAsync(filme)
            );
        }

        private async Task<bool> InsertFilmeAsync(Filme filme)
        {
            await _mongoContext.GetCollection<Filme>().InsertOneAsync(filme);

            return true;
        }
    }
}
