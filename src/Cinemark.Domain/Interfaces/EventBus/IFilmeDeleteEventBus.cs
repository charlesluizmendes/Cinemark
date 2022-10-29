using Cinemark.Domain.Models;

namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface IFilmeDeleteEventBus : IBaseEventBus<Filme>
    {
        Task<bool> HandlerMessageAsync(Filme filme);
    }
}
