using Cinemark.Domain.Models;

namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface IFilmeCreateEventBus : IBaseEventBus<Filme>
    {
        Task HandlerMessageAsync(Filme filme);
    }
}
