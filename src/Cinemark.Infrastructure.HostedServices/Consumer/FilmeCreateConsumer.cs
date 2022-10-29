using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Models;
using Microsoft.Extensions.Hosting;

namespace Cinemark.Infrastructure.Services.Consumer
{
    public class FilmeCreateConsumer : BackgroundService
    {
        private readonly IFilmeCreateEventBus _filmeCreateEventBus;

        public FilmeCreateConsumer(IFilmeCreateEventBus filmeCreateEventBus)
        {
            _filmeCreateEventBus = filmeCreateEventBus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _filmeCreateEventBus.SubscriberAsync(async (filme, stoppingToken) =>
                await _filmeCreateEventBus.HandlerMessageAsync(filme)
            );        
        }
    }
}
