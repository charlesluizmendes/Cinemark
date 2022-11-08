using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class DeleteFilmeCommand : IRequest<ResultData<Filme>>
    {
        public Filme Filme { get; set; } = null!;
    }
}
