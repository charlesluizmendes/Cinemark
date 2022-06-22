using Cinemark.Domain.Entities;
using Cinemark.Domain.Interfaces.Repositories;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class DeleteFilmeCommandHandler : IRequestHandler<DeleteFilmeCommand, Filme>
    {
        private readonly IFilmeRepository _filmeRepository;

        public DeleteFilmeCommandHandler(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public async Task<Filme> Handle(DeleteFilmeCommand request, CancellationToken cancellationToken)
        {
            return await _filmeRepository.DeleteAsync(request.Filme);
        }
    }
}
