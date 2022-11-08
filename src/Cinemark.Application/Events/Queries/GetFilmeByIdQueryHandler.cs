using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;
using MediatR;

namespace Cinemark.Application.Events.Queries
{
    public class GetFilmeByIdQueryHandler : IRequestHandler<GetFilmeByIdQuery, ResultData<Filme>>
    {
        private readonly IFilmeRepository _filmeRepository;

        public GetFilmeByIdQueryHandler(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public async Task<ResultData<Filme>> Handle(GetFilmeByIdQuery request, CancellationToken cancellationToken)
        {
            var filme = await _filmeRepository.GetByIdAsync(request.Id);

            if (filme == null)
                return new ErrorData<Filme>("O Filme não foi encontrado");

            return new SuccessData<Filme>(filme);
        }
    }
}
