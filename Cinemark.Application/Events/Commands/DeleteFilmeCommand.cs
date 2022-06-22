using Cinemark.Domain.Models;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class DeleteFilmeCommand : IRequest<Filme>
    {
        public Filme Filme { get; set; } = null!;
    }
}
