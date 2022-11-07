using Cinemark.Domain.Models;

namespace Cinemark.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario> GetUsuarioByEmailAndSenhaAsync(Usuario usuario);
    }
}
