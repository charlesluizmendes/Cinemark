using Cinemark.Domain.Constants;
using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class UpdateFilmeCommandHandler : IRequestHandler<UpdateFilmeCommand, ResultData<Filme>>
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly IFilmeEventBus _filmeEventBus;

        public UpdateFilmeCommandHandler(IFilmeRepository filmeRepository,
            IFilmeEventBus filmeEventBus)
        {
            _filmeRepository = filmeRepository;
            _filmeEventBus = filmeEventBus;
        }

        public async Task<ResultData<Filme>> Handle(UpdateFilmeCommand request, CancellationToken cancellationToken)
        {
            var filme = await _filmeRepository.UpdateAsync(request.Filme);

            if (filme == null)
                return new ErrorData<Filme>("Não foi possível alterar o Filme");

            await _filmeEventBus.PublisherAsync(typeof(Filme).Name + QueueConstants.Update, filme);

            return new SuccessData<Filme>(filme);
        }
    }
}
