using Cinemark.Domain.Entities;
using Cinemark.Domain.Interfaces.Services;
using MediatR;

namespace Cinemark.Application.Events.Queries
{
    public class GetFilmeByIdQueryHandler : IRequestHandler<GetFilmeByIdQuery, Filme>
    {
        private readonly IFilmeService _filmeService;

        public GetFilmeByIdQueryHandler(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        public async Task<Filme> Handle(GetFilmeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _filmeService.GetByIdAsync(request.Id);
        }
    }
}
