using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Cinemark.Domain.Commom;
using Cinemark.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Cinemark.Infrastructure.Data.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly MongoContext _mongoContext;
        private readonly SqlServerContext _sqlServerContext;
        public IUnitOfWork UnitOfWork => _sqlServerContext;

        public FilmeRepository(MongoContext mongoContext, SqlServerContext sqlServerContext) 
        {
            _mongoContext = mongoContext ?? throw new ArgumentNullException(nameof(mongoContext));
            _sqlServerContext = sqlServerContext ?? throw new ArgumentNullException(nameof(sqlServerContext));
        }

        public async Task<IEnumerable<Filme>> GetAllAsync()
        {
            return await _mongoContext.GetCollection<Filme>().Find(Builders<Filme>.Filter.Empty).ToListAsync();
        }

        public async Task<Filme> GetByIdAsync(Guid id)
        {
            return await _mongoContext.GetCollection<Filme>().Find(Builders<Filme>.Filter.Eq("_id", id.ToString())).FirstOrDefaultAsync();
        }

        public async Task InsertAsync(Filme filme)
        {
            await _sqlServerContext.Filme.AddAsync(filme);
        }

        public void Update(Filme filme)
        {
            _sqlServerContext.Filme.Update(filme);
        }

        public void Delete(Filme filme)
        {
            _sqlServerContext.Filme.Remove(filme);
        }
    }
}
