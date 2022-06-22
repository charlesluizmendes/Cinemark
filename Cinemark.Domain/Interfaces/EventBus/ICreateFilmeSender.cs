using Cinemark.Domain.Entities;

namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface ICreateFilmeSender
    {
        Task SendMessageAsync(Filme filme);
    }
}
