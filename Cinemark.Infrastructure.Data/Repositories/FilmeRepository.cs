using Cinemark.Domain.Entities;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Infrastructure.Data.Context;
using MongoDB.Driver;

namespace Cinemark.Infrastructure.Data.Repositories
{
    public class FilmeRepository : BaseRepository<Filme>, IFilmeRepository
    {
        private readonly CinemarkMongoContext _mongoContext;
        private IMongoCollection<Filme> _dbMongoCollection;
        private readonly CinemarkSqlServerContext _sqlServercontext;

        public FilmeRepository(CinemarkMongoContext mongoContext, 
            CinemarkSqlServerContext sqlServercontext) 
            : base(mongoContext, sqlServercontext)
        {
            _mongoContext = mongoContext;
            _dbMongoCollection = _mongoContext.GetCollection<Filme>(typeof(Filme).Name);
            _sqlServercontext = sqlServercontext;
        }
    }
}
