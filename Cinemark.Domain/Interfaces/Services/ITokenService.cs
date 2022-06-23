using Cinemark.Domain.Models;

namespace Cinemark.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        Task<Token> CreateTokenAsync(Usuario usuario);
    }
}
