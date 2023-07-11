namespace Cinemark.Domain.AggregatesModels.UsuarioAggregate
{
    public interface IUsuarioService
    {
        Task<Token> CreateTokenAsync(string email, string senha);
    }
}
