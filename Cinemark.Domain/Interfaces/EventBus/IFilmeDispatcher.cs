using Cinemark.Domain.Entities;

namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface IFilmeDispatcher
    {
        Task SendMessageAsync(Filme filme);
    }
}
