using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using MediatR;

namespace Cinemark.Application.Queries
{
    public class GetFilmeByIdQueryHandler : IRequestHandler<GetFilmeByIdQuery, Filme>
    {
        private readonly IFilmeRepository _filmeRepository;

        public GetFilmeByIdQueryHandler(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public async Task<Filme> Handle(GetFilmeByIdQuery request, CancellationToken cancellationToken)
        {
            var filme = await _filmeRepository.GetByIdAsync(request.Id);

            if (filme is null)
                throw new KeyNotFoundException("Filme não encontrado");

            return filme;
        }
    }
}
