using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Cinemark.Domain.Contracts.Events;
using MediatR;

namespace Cinemark.Application.EventHandlers
{
    public class FilmeCreatedEventHandler : 
        INotificationHandler<FilmeCreatedEvent>
    {
        private readonly IFilmeEventBus _eventBus;

        public FilmeCreatedEventHandler(IFilmeEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(FilmeCreatedEvent notification, CancellationToken cancellationToken)
        {
            var queueName = typeof(Filme).Name + FilmeQueue.Created.Name;

            await _eventBus.PublisherAsync(queueName, notification.Filme);
        }
    }
}
