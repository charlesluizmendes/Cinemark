using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using MediatR;

namespace Cinemark.Domain.Contracts.Events
{
    public class FilmeRemovedEvent :
        INotification
    {
        public Filme Filme { get; }

        public FilmeRemovedEvent(Filme filme)
        {
            Filme = filme;
        }
    }
}
