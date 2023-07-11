using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Cinemark.Domain.Events;
using MediatR;

namespace Cinemark.Application.Events
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
