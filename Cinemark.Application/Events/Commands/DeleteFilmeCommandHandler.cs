using Cinemark.Domain.Entities;
using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class DeleteFilmeCommandHandler : IRequestHandler<DeleteFilmeCommand, Filme>
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly IDeleteFilmeSender _deleteFilmeSender;

        public DeleteFilmeCommandHandler(IFilmeRepository filmeRepository,
            IDeleteFilmeSender deleteFilmeSender)
        {
            _filmeRepository = filmeRepository;
            _deleteFilmeSender = deleteFilmeSender;
        }

        public async Task<Filme> Handle(DeleteFilmeCommand request, CancellationToken cancellationToken)
        {
            var result = await _filmeRepository.DeleteAsync(request.Filme);
            await _deleteFilmeSender.SendMessageAsync(result);

            return result;
        }
    }
}
