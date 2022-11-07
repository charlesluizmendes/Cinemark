using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;

namespace Cinemark.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<ResultData<Usuario>> GetUsuarioByEmailAndSenhaAsync(Usuario usuario);
    }
}
