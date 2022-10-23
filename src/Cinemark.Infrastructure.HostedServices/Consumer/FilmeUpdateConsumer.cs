using Cinemark.Domain.Interfaces.EventBus;
using Microsoft.Extensions.Hosting;

namespace Cinemark.Infrastructure.Services.Consumer
{
    public class FilmeUpdateConsumer : BackgroundService
    {
        private readonly IFilmeUpdateEventBus _filmeUpdateEventBus;

        public FilmeUpdateConsumer(IFilmeUpdateEventBus filmeUpdateEventBus)
        {
            _filmeUpdateEventBus = filmeUpdateEventBus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _filmeUpdateEventBus.HandleMessageAsync();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
