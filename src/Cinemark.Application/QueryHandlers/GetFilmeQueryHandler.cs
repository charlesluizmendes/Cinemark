using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Cinemark.Domain.Contracts.Queries;
using MediatR;

namespace Cinemark.Application.QueryHandlers
{
    public class GetFilmeQueryHandler : IRequestHandler<GetFilmeQuery, IEnumerable<Filme>>
    {
        private readonly IFilmeRepository _filmeRepository;

        public GetFilmeQueryHandler(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public async Task<IEnumerable<Filme>> Handle(GetFilmeQuery request, CancellationToken cancellationToken)
        {
            return await _filmeRepository.GetAllAsync();
        }
    }
}
