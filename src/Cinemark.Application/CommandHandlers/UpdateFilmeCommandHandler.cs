using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Cinemark.Domain.Contracts.Commands;
using Cinemark.Domain.Contracts.Events;
using MediatR;

namespace Cinemark.Application.CommandHandlers
{
    public class UpdateFilmeCommandHandler : IRequestHandler<UpdateFilmeCommand, bool>
    {
        private readonly IFilmeRepository _filmeRepository;

        public UpdateFilmeCommandHandler(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public async Task<bool> Handle(UpdateFilmeCommand request, CancellationToken cancellationToken)
        {
            var filme = await _filmeRepository.GetByIdAsync(request.Id);

            if (filme is null)
                throw new KeyNotFoundException("Filme não encontrado");

            filme.Update(request.Id, request.Nome, request.Categoria, request.FaixaEtaria, request.DataLancamento);

            _filmeRepository.Update(filme);
            filme.AddDomainEvent(new FilmeUpdatedEvent(filme));

            return await _filmeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
