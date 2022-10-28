using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class CreateFilmeCommandHandler : IRequestHandler<CreateFilmeCommand, Filme>
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly IFilmeCreateEventBus _filmeCreateEventBus;

        public CreateFilmeCommandHandler(IFilmeRepository filmeRepository,
            IFilmeCreateEventBus filmeCreateEventBus)
        {
            _filmeRepository = filmeRepository;
            _filmeCreateEventBus = filmeCreateEventBus;
        }

        public async Task<Filme> Handle(CreateFilmeCommand request, CancellationToken cancellationToken)
        {
            var result = await _filmeRepository.InsertAsync(request.Filme);
            await _filmeCreateEventBus.PublisherAsync(result);

            return result;
        }
    }
}
