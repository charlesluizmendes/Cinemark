using MediatR;

namespace Cinemark.Domain.Contracts.Commands
{
    public class DeleteFilmeCommand : IRequest<bool>
    {
        public Guid Id { get; private set; }

        public DeleteFilmeCommand(Guid id)
        {
            Id = id;
        }
    }
}
