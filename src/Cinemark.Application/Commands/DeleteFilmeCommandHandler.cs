using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Cinemark.Domain.Events;
using MediatR;

namespace Cinemark.Application.Commands
{
    public class DeleteFilmeCommandHandler : IRequestHandler<DeleteFilmeCommand, bool>
    {
        private readonly IFilmeRepository _filmeRepository;

        public DeleteFilmeCommandHandler(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public async Task<bool> Handle(DeleteFilmeCommand request, CancellationToken cancellationToken)
        {
            var filme = await _filmeRepository.GetByIdAsync(request.Id);

            if (filme is null)
                throw new KeyNotFoundException("Filme não econtrado");

            filme.Delete(request.Id);

            _filmeRepository.Delete(filme);
            filme.AddDomainEvent(new FilmeRemovedEvent(filme));

            return await _filmeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
