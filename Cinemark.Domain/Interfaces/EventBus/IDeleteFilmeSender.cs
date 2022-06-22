using Cinemark.Domain.Models;

namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface IDeleteFilmeSender
    {
        Task SendMessageAsync(Filme filme);

    }
}
