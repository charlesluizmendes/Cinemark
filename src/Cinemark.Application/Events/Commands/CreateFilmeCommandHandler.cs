using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class CreateFilmeCommandHandler : IRequestHandler<CreateFilmeCommand, Filme>
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly IFilmeEventBus _filmeEventBus;

        public CreateFilmeCommandHandler(IFilmeRepository filmeRepository,
            IFilmeEventBus filmeEventBus)
        {
            _filmeRepository = filmeRepository;
            _filmeEventBus = filmeEventBus;
        }

        public async Task<Filme> Handle(CreateFilmeCommand request, CancellationToken cancellationToken)
        {
            var filme = await _filmeRepository.InsertAsync(request.Filme);
            await _filmeEventBus.PublisherAsync(typeof(Filme).Name + "_Insert", filme);

            return filme;
        }
    }
}
