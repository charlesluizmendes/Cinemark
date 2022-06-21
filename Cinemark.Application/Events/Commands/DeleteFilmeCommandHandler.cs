using Cinemark.Domain.Entities;
using Cinemark.Domain.Interfaces.Services;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class DeleteFilmeCommandHandler : IRequestHandler<DeleteFilmeCommand, Filme>
    {
        private readonly IFilmeService _filmeService;

        public DeleteFilmeCommandHandler(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        public async Task<Filme> Handle(DeleteFilmeCommand request, CancellationToken cancellationToken)
        {
            return await _filmeService.DeleteAsync(request.Filme);
        }
    }
}
