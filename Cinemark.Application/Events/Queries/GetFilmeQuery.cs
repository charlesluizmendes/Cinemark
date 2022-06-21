using Cinemark.Domain.Entities;
using MediatR;

namespace Cinemark.Application.Events.Queries
{
    public class GetFilmeQuery : IRequest<IEnumerable<Filme>>
    {
    }
}
