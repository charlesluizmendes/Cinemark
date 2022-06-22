using Cinemark.Domain.Entities;
using Cinemark.Domain.Interfaces.Repositories;
using MediatR;

namespace Cinemark.Application.Events.Queries
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
