using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Cinemark.Domain.Events;
using MediatR;

namespace Cinemark.Application.Commands
{
    public class CreateFilmeCommandHandler : IRequestHandler<CreateFilmeCommand, bool>
    {
        private readonly IFilmeRepository _filmeRepository;

        public CreateFilmeCommandHandler(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public async Task<bool> Handle(CreateFilmeCommand request, CancellationToken cancellationToken)
        {
            var filme = new Filme(Guid.NewGuid(), request.Nome, request.Categoria, request.FaixaEtaria, request.DataLancamento);

            await _filmeRepository.InsertAsync(filme);
            filme.AddDomainEvent(new FilmeCreatedEvent(filme));

            return await _filmeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
