using Cinemark.Domain.Entities;
using Cinemark.Domain.Interfaces.Services;
using MediatR;

namespace Cinemark.Application.Events.Queries
{
    public class GetFilmeQueryHandler : IRequestHandler<GetFilmeQuery, IEnumerable<Filme>>
    {
        private readonly IFilmeService _filmeService;

        public GetFilmeQueryHandler(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        public async Task<IEnumerable<Filme>> Handle(GetFilmeQuery request, CancellationToken cancellationToken)
        {
            return await _filmeService.GetAllAsync();
        }
    }
}
