using Cinemark.Domain.Entities;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class CreateFilmeCommand : IRequest<Filme>
    {
        public Filme Filme { get; set; } = null!;
    }
}
