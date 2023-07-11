using Cinemark.Domain.Core.Commom;

namespace Cinemark.Domain.AggregatesModels.UsuarioAggregate
{
    public interface IUsuarioRepository : 
        IRepository<Usuario>
    {
        Task<Usuario> GetUsuarioByEmailAndSenhaAsync(string email, string senha);
    }
}
