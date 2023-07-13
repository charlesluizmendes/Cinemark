using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using MediatR;

namespace Cinemark.Domain.Contracts.Queries
{
    public class GetFilmeQuery : IRequest<IEnumerable<Filme>>
    {
        public GetFilmeQuery() { }
    }
}
