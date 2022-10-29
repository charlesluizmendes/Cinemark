using Cinemark.Domain.Models;

namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface IFilmeUpdateEventBus : IBaseEventBus<Filme>
    {
        Task<bool> HandlerMessageAsync(Filme filme);
    }
}
