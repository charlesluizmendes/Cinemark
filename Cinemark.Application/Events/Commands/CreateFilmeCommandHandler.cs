using Cinemark.Domain.Entities;
using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class CreateFilmeCommandHandler : IRequestHandler<CreateFilmeCommand, Filme>
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly ICreateFilmeSender _createFilmeSender;

        public CreateFilmeCommandHandler(IFilmeRepository filmeRepository, 
            ICreateFilmeSender createFilmeSender)
        {
            _filmeRepository = filmeRepository;
            _createFilmeSender = createFilmeSender;
        }

        public async Task<Filme> Handle(CreateFilmeCommand request, CancellationToken cancellationToken)
        {
            var result = await _filmeRepository.InsertAsync(request.Filme);
            await _createFilmeSender.SendMessageAsync(result);

            return result;
        }
    }
}
