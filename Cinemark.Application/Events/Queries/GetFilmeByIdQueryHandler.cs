using Cinemark.Domain.Entities;
using Cinemark.Domain.Interfaces.Repositories;
using MediatR;

namespace Cinemark.Application.Events.Queries
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
            return await _filmeRepository.GetByIdAsync(request.Id);
        }
    }
}
