using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class UpdateFilmeCommandHandler : IRequestHandler<UpdateFilmeCommand, Filme>
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly IFilmeEventBus _filmeEventBus;

        public UpdateFilmeCommandHandler(IFilmeRepository filmeRepository,
            IFilmeEventBus filmeEventBus)
        {
            _filmeRepository = filmeRepository;
            _filmeEventBus = filmeEventBus;
        }

        public async Task<Filme> Handle(UpdateFilmeCommand request, CancellationToken cancellationToken)
        {
            var filme = await _filmeRepository.UpdateAsync(request.Filme);
            await _filmeEventBus.PublisherAsync(typeof(Filme).Name + "_Update", filme);

            return filme;
        }
    }
}
