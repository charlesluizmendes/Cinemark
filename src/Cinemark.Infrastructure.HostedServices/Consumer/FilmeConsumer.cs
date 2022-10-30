using Cinemark.Domain.Interfaces.EventBus;
using Microsoft.Extensions.Hosting;

namespace Cinemark.Infrastructure.Services.Consumer
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
            await _filmeEventBus.SubscriberAsync("Filme_Insert", async (filme, stoppingToken) => 
                await _filmeEventBus.HandlerInsertAsync(filme)
            );

            await _filmeEventBus.SubscriberAsync("Filme_Update", async (filme, stoppingToken) =>
                await _filmeEventBus.HandlerUpdateAsync(filme)
            );

            await _filmeEventBus.SubscriberAsync("Filme_Delete", async (filme, stoppingToken) =>
                await _filmeEventBus.HandlerDeleteAsync(filme)
            );
        }
    }
}
