using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;
using MediatR;

namespace Cinemark.Application.Events.Queries
{
    public class GetFilmeByIdQuery : IRequest<ResultData<Filme>>
    {
        public int Id { get; set; }
    }
}
