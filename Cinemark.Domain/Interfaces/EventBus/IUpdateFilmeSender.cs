using Cinemark.Domain.Entities;

namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface IUpdateFilmeSender
    {
        Task SendMessageAsync(Filme filme);

    }
}
