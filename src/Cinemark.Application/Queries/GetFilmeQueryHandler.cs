using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using MediatR;

namespace Cinemark.Application.Queries
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
