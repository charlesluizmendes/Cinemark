using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using MediatR;

namespace Cinemark.Domain.Events
{
    public class FilmeCreatedEvent : 
        INotification
    {
        public Filme Filme { get; }

        public FilmeCreatedEvent(Filme filme)
        {
            Filme = filme;
        }
    }
}
