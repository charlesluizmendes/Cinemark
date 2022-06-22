using Cinemark.Domain.Entities;

namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface IDeleteFilmeSender
    {
        Task SendMessageAsync(Filme filme);

    }
}
