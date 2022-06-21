using Cinemark.Domain.Entities;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Interfaces.Services;

namespace Cinemark.Domain.Services
{
    public class FilmeService : BaseService<Filme>, IFilmeService
    {
        private readonly IFilmeRepository _filmeRepository;

        public FilmeService(IFilmeRepository filmeRepository)
            : base(filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }
    }
}
