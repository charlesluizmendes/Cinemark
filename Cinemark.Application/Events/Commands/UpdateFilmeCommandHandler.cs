using Cinemark.Domain.Entities;
using Cinemark.Domain.Interfaces.Services;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class UpdateFilmeCommandHandler : IRequestHandler<UpdateFilmeCommand, Filme>
    {
        private readonly IFilmeService _filmeService;

        public UpdateFilmeCommandHandler(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        public async Task<Filme> Handle(UpdateFilmeCommand request, CancellationToken cancellationToken)
        {
            return await _filmeService.UpdateAsync(request.Filme);
        }
    }
}
