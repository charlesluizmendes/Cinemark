using Cinemark.Domain.Entities;
using Cinemark.Domain.Interfaces.Services;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class CreateFilmeCommandHandler : IRequestHandler<CreateFilmeCommand, Filme>
    {
        private readonly IFilmeService _filmeService;

        public CreateFilmeCommandHandler(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        public async Task<Filme> Handle(CreateFilmeCommand request, CancellationToken cancellationToken)
        {
            return await _filmeService.InsertAsync(request.Filme);
        }
    }
}
