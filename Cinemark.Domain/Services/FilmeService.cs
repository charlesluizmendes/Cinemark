using Cinemark.Domain.Entities;
using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Interfaces.Services;

namespace Cinemark.Domain.Services
{
    public class FilmeService : BaseService<Filme>, IFilmeService
    {
        private readonly IFilmeRepository? _filmeRepository;
        private readonly IFilmeDispatcher? _createFilmeSender;

        public FilmeService(IFilmeRepository filmeRepository)
            : base(filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public override async Task<Filme> InsertAsync(Filme filme)
        {
            var result = await _filmeRepository.InsertAsync(filme);

            if (result != null)            
                await _createFilmeSender.SendMessageAsync(filme);

            return result;
        }
    }
}
