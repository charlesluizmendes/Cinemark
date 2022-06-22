using Cinemark.Domain.Entities;
using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class UpdateFilmeCommandHandler : IRequestHandler<UpdateFilmeCommand, Filme>
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly IUpdateFilmeSender _updateFilmeSender;

        public UpdateFilmeCommandHandler(IFilmeRepository filmeRepository,
            IUpdateFilmeSender updateFilmeSender)
        {
            _filmeRepository = filmeRepository;
            _updateFilmeSender = updateFilmeSender;
        }

        public async Task<Filme> Handle(UpdateFilmeCommand request, CancellationToken cancellationToken)
        {
            var result = await _filmeRepository.UpdateAsync(request.Filme);
            await _updateFilmeSender.SendMessageAsync(result);

            return result;
        }
    }
}
