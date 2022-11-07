using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Interfaces.Services;
using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;
using MediatR;

namespace Cinemark.Application.Events.Queries
{
    public class GetTokenByUsuarioQueryHandler : IRequestHandler<GetTokenByUsuarioQuery, ResultData<Token>>
    {
        private readonly ITokenService _tokenService;
        private readonly IUsuarioRepository _usuarioRepository;

        public GetTokenByUsuarioQueryHandler(ITokenService tokenService, 
            IUsuarioRepository usuarioRepository)
        {
            _tokenService = tokenService;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<ResultData<Token>> Handle(GetTokenByUsuarioQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetUsuarioByEmailAndSenhaAsync(request.Usuario);

            if (!usuario.Success)
                return new ErrorData<Token>("Usuário e/ou senha inválidos");

            var token = await _tokenService.CreateTokenAsync(usuario.Data);

            if (!token.Success)
                return new ErrorData<Token>("Não foi possível criar o Token");

            return new SuccessData<Token>(token.Data);
        }
    }
}
