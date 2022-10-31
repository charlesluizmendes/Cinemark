using Cinemark.Domain.Models;

namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface IFilmeEventBus : IBaseEventBus<Filme>
    {
        Task<bool> HandlerInsertAsync(Filme filme);
        Task<bool> HandlerUpdateAsync(Filme filme);
        Task<bool> HandlerDeleteAsync(Filme filme);
    }
}
