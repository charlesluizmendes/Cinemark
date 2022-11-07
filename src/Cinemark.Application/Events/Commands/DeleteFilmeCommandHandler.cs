using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class DeleteFilmeCommandHandler : IRequestHandler<DeleteFilmeCommand, ResultData<Filme>>
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly IFilmeEventBus _filmeEventBus;

        public DeleteFilmeCommandHandler(IFilmeRepository filmeRepository,
            IFilmeEventBus filmeEventBus)
        {
            _filmeRepository = filmeRepository;
            _filmeEventBus = filmeEventBus;
        }

        public async Task<ResultData<Filme>> Handle(DeleteFilmeCommand request, CancellationToken cancellationToken)
        {
            var filme = await _filmeRepository.DeleteAsync(request.Filme);

            if (!filme.Success)
                return new ErrorData<Filme>("O Filme não foi encontrado");

            await _filmeEventBus.PublisherAsync(typeof(Filme).Name + "_Delete", filme.Data);

            return new SuccessData<Filme>(filme.Data);
        }
    }
}
