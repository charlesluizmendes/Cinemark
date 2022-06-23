using Cinemark.Domain.Models;

namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface ICreateFilmeSender
    {
        Task SendMessageAsync(Filme filme);
    }
}
