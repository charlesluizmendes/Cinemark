using Cinemark.Domain.Constants;
using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Models;
using Microsoft.Extensions.Hosting;

namespace Cinemark.Infrastructure.HostedServices.Consumer
{
    public class FilmeConsumer : BackgroundService
    {
        private readonly IFilmeEventBus _filmeEventBus;

        public FilmeConsumer(IFilmeEventBus filmeEventBus)
        {
            _filmeEventBus = filmeEventBus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _filmeEventBus.SubscriberAsync(typeof(Filme).Name + QueueConstants.Insert, async (filme, stoppingToken) => 
                await _filmeEventBus.HandlerInsertAsync(filme)
            );

            await _filmeEventBus.SubscriberAsync(typeof(Filme).Name + QueueConstants.Update, async (filme, stoppingToken) =>
                await _filmeEventBus.HandlerUpdateAsync(filme)
            );

            await _filmeEventBus.SubscriberAsync(typeof(Filme).Name + QueueConstants.Delete, async (filme, stoppingToken) =>
                await _filmeEventBus.HandlerDeleteAsync(filme)
            );
        }
    }
}
