using Cinemark.Domain.Models;

namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface IFilmeDeleteEventBus : IBaseEventBus<Filme>
    {
        Task HandlerMessageAsync(Filme filme);
    }
}
