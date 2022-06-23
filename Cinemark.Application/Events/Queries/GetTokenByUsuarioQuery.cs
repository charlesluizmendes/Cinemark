using Cinemark.Domain.Models;
using MediatR;

namespace Cinemark.Application.Events.Queries
{
    public class GetTokenByUsuarioQuery : IRequest<Token>
    {
        public Usuario Usuario { get; set; } = null!;
    }
}
