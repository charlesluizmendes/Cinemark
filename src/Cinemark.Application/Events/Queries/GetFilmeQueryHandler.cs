using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;
using MediatR;

namespace Cinemark.Application.Events.Queries
{
    public class GetFilmeQueryHandler : IRequestHandler<GetFilmeQuery, ResultData<IEnumerable<Filme>>>
    {
        private readonly IFilmeRepository _filmeRepository;

        public GetFilmeQueryHandler(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public async Task<ResultData<IEnumerable<Filme>>> Handle(GetFilmeQuery request, CancellationToken cancellationToken)
        {
            var filme = await _filmeRepository.GetAllAsync();

            if (!filme.Success)
                return new ErrorData<IEnumerable<Filme>>("Nenhum Filme foi encontrado");

            return new SuccessData<IEnumerable<Filme>>(filme.Data);
        }
    }
}
