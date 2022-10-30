using System;
using Cinemark.Domain.Models;

namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface IUsuarioEventBus : IBaseEventBus<Usuario>
    {
        Task<bool> HandlerInsertAsync(Usuario usuario);
        Task<bool> HandlerUpdateAsync(Usuario usuario);
        Task<bool> HandlerDeleteAsync(Usuario usuario);
    }
}

