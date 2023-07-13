using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Cinemark.Domain.Contracts.Queries;
using MediatR;

namespace Cinemark.Application.QueryHandlers
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
