using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Interfaces.Services;
using Cinemark.Domain.Models;
using MediatR;

namespace Cinemark.Application.Events.Queries
{
    public class GetTokenByUsuarioQueryHandler : IRequestHandler<GetTokenByUsuarioQuery, Token?>
    {
        private readonly ITokenService _tokenService;
        private readonly IUsuarioRepository _usuarioRepository;

        public GetTokenByUsuarioQueryHandler(ITokenService tokenService, 
            IUsuarioRepository usuarioRepository)
        {
            _tokenService = tokenService;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Token?> Handle(GetTokenByUsuarioQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetUsuarioByEmailAndSenhaAsync(request.Usuario);

            if (usuario == null)
                return null;

            return await _tokenService.CreateTokenAsync(usuario);
        }
    }
}
