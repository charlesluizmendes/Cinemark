using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Cinemark.Domain.Events;
using MediatR;

namespace Cinemark.Application.Events
{
    public class FilmeRemovedEventHandler : 
        INotificationHandler<FilmeRemovedEvent>
    {
        private readonly IFilmeEventBus _eventBus;

        public FilmeRemovedEventHandler(IFilmeEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(FilmeRemovedEvent notification, CancellationToken cancellationToken)
        {
            var queueName = typeof(Filme).Name + FilmeQueue.Removed.Name;

            await _eventBus.PublisherAsync(queueName, notification.Filme);
        }
    }
}
