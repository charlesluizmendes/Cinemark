using Cinemark.Domain.Commom;

namespace Cinemark.Domain.AggregatesModels.FilmeAggregate
{
    public interface IFilmeRepository : 
        IRepository<Filme>
    {
        Task<IEnumerable<Filme>> GetAllAsync();
        Task<Filme> GetByIdAsync(Guid id);
        Task InsertAsync(Filme filme);
        void Update(Filme filme);
        void Delete(Filme filme);
    }
}
