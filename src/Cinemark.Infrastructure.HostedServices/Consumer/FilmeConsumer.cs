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
            await _filmeEventBus.SubscriberAsync(typeof(Usuario).Name + "_Insert", async (filme, stoppingToken) => 
                await _filmeEventBus.HandlerInsertAsync(filme)
            );

            await _filmeEventBus.SubscriberAsync(typeof(Usuario).Name + "_Update", async (filme, stoppingToken) =>
                await _filmeEventBus.HandlerUpdateAsync(filme)
            );

            await _filmeEventBus.SubscriberAsync(typeof(Usuario).Name + "_Delete", async (filme, stoppingToken) =>
                await _filmeEventBus.HandlerDeleteAsync(filme)
            );
        }
    }
}
