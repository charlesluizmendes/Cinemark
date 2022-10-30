using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class UpdateFilmeCommandHandler : IRequestHandler<UpdateFilmeCommand, Filme>
    {
        private readonly IFilmeRepository _filmeRepository;

        public UpdateFilmeCommandHandler(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public async Task<Filme> Handle(UpdateFilmeCommand request, CancellationToken cancellationToken)
        {
            return await _filmeRepository.UpdateAsync(request.Filme);
        }
    }
}
