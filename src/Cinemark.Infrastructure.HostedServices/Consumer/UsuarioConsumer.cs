using System;
using Cinemark.Domain.Interfaces.EventBus;
using Microsoft.Extensions.Hosting;

namespace Cinemark.Infrastructure.HostedServices.Consumer
{
    public class UsuarioConsumer : BackgroundService
    {
        private readonly IUsuarioEventBus _usuarioEventBus;

        public UsuarioConsumer(IUsuarioEventBus usuarioEventBus)
        {
            _usuarioEventBus = usuarioEventBus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _usuarioEventBus.SubscriberAsync("Usuario_Insert", async (usuario, stoppingToken) =>
                await _usuarioEventBus.HandlerInsertAsync(usuario)
            );

            await _usuarioEventBus.SubscriberAsync("Usuario_Update", async (usuario, stoppingToken) =>
                await _usuarioEventBus.HandlerUpdateAsync(usuario)
            );

            await _usuarioEventBus.SubscriberAsync("Usuario_Delete", async (usuario, stoppingToken) =>
                await _usuarioEventBus.HandlerDeleteAsync(usuario)
            );
        }
    }
}

