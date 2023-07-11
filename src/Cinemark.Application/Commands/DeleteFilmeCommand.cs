using MediatR;

namespace Cinemark.Application.Commands
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
