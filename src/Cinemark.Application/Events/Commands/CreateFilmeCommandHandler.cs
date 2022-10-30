using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class CreateFilmeCommandHandler : IRequestHandler<CreateFilmeCommand, Filme>
    {
        private readonly IFilmeRepository _filmeRepository;

        public CreateFilmeCommandHandler(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public async Task<Filme> Handle(CreateFilmeCommand request, CancellationToken cancellationToken)
        {
            return await _filmeRepository.InsertAsync(request.Filme);
        }
    }
}
