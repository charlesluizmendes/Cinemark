using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;

namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface IFilmeEventBus : IBaseEventBus<Filme>
    {
        Task<ResultData<bool>> HandlerInsertAsync(Filme filme);
        Task<ResultData<bool>> HandlerUpdateAsync(Filme filme);
        Task<ResultData<bool>> HandlerDeleteAsync(Filme filme);
    }
}
