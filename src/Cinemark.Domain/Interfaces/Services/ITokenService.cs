using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;

namespace Cinemark.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        Task<ResultData<Token>> CreateTokenAsync(Usuario usuario);
    }
}
