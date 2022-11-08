using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;
using MediatR;

namespace Cinemark.Application.Events.Queries
{
    public class GetTokenByUsuarioQuery : IRequest<ResultData<Token>>
    {
        public Usuario Usuario { get; set; } = null!;
    }
}
