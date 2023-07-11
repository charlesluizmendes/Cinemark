using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using MediatR;

namespace Cinemark.Application.Queries
{
    public class GetFilmeQuery : IRequest<IEnumerable<Filme>>
    {
        public GetFilmeQuery() { }
    }
}
