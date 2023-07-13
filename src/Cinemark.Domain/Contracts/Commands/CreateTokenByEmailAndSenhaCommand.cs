using Cinemark.Domain.AggregatesModels.UsuarioAggregate;
using MediatR;

namespace Cinemark.Domain.Contracts.Commands
{
    public class CreateTokenByEmailAndSenhaCommand : IRequest<Token>
    {
        public string Email { get; private set; }
        public string Senha { get; private set; }

        public CreateTokenByEmailAndSenhaCommand(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
    }
}
