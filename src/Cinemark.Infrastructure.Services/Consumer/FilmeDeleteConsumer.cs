using Cinemark.Domain.Interfaces.EventBus;
using Microsoft.Extensions.Hosting;

namespace Cinemark.Infrastructure.Services.Consumer
{
    public class FilmeDeleteConsumer : BackgroundService
    {
        private readonly IFilmeDeleteEventBus _filmeDeleteEventBus;

        public FilmeDeleteConsumer(IFilmeDeleteEventBus filmeDeleteEventBus)
        {
            _filmeDeleteEventBus = filmeDeleteEventBus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _filmeDeleteEventBus.ReadMessgaesAsync();
        }
    }
}
