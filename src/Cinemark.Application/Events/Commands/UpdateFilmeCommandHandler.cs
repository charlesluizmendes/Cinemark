using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class UpdateFilmeCommandHandler : IRequestHandler<UpdateFilmeCommand, Filme>
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly IFilmeUpdateEventBus _filmeUpdateEventBus;

        public UpdateFilmeCommandHandler(IFilmeRepository filmeRepository,
            IFilmeUpdateEventBus filmeUpdateEventBus)
        {
            _filmeRepository = filmeRepository;
            _filmeUpdateEventBus = filmeUpdateEventBus;
        }

        public async Task<Filme> Handle(UpdateFilmeCommand request, CancellationToken cancellationToken)
        {
            var result = await _filmeRepository.UpdateAsync(request.Filme);
            await _filmeUpdateEventBus.SendMessageAsync(result);

            return result;
        }
    }
}
