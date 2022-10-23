using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class DeleteFilmeCommandHandler : IRequestHandler<DeleteFilmeCommand, Filme>
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly IFilmeDeleteEventBus _filmeDeleteEventBus;

        public DeleteFilmeCommandHandler(IFilmeRepository filmeRepository,
            IFilmeDeleteEventBus filmeDeleteEventBus)
        {
            _filmeRepository = filmeRepository;
            _filmeDeleteEventBus = filmeDeleteEventBus;
        }

        public async Task<Filme> Handle(DeleteFilmeCommand request, CancellationToken cancellationToken)
        {
            var result = await _filmeRepository.DeleteAsync(request.Filme);
            await _filmeDeleteEventBus.PublisherAsync(result);

            return result;
        }
    }
}
