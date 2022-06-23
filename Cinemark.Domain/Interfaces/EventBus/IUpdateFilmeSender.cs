using Cinemark.Domain.Models;

namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface IUpdateFilmeSender
    {
        Task SendMessageAsync(Filme filme);

    }
}
