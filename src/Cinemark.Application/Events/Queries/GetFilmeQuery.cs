using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;
using MediatR;

namespace Cinemark.Application.Events.Queries
{
    public class GetFilmeQuery : IRequest<ResultData<IEnumerable<Filme>>>
    {
    }
}
