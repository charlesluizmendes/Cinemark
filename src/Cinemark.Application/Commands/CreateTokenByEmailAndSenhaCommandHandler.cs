using Cinemark.Domain.AggregatesModels.UsuarioAggregate;
using MediatR;

namespace Cinemark.Application.Commands
{
    public class CreateTokenByEmailAndSenhaCommandHandler : IRequestHandler<CreateTokenByEmailAndSenhaCommand, Token>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _loginService;

        public CreateTokenByEmailAndSenhaCommandHandler(IUsuarioRepository usuarioRepository, 
            IUsuarioService loginService)
        {
            _usuarioRepository = usuarioRepository;
            _loginService = loginService;
        }

        public async Task<Token> Handle(CreateTokenByEmailAndSenhaCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetUsuarioByEmailAndSenhaAsync(request.Email, request.Senha);

            if (usuario is null)
                throw new KeyNotFoundException("Usuário e/ou senha inválidos");

            return await _loginService.CreateTokenAsync(request.Email, request.Senha);
        }
    }
}
