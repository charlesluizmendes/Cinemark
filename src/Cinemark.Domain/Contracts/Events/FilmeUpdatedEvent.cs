using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using MediatR;

namespace Cinemark.Domain.Contracts.Events
{
    public class FilmeUpdatedEvent :
        INotification
    {
        public Filme Filme { get; }

        public FilmeUpdatedEvent(Filme filme)
        {
            Filme = filme;
        }
    }
}
