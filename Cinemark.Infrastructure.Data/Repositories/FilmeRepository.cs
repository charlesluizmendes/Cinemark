using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using Cinemark.Infrastructure.Data.Context;
using MongoDB.Driver;

namespace Cinemark.Infrastructure.Data.Repositories
{
    public class FilmeRepository : BaseRepository<Filme>, IFilmeRepository
    {
        private readonly MongoContext _mongoContext;
        private IMongoCollection<Filme> _mongoCollection;
        private readonly SqlServerContext _sqlServercontext;

        public FilmeRepository(MongoContext mongoContext,
            SqlServerContext sqlServercontext) 
            : base(mongoContext, sqlServercontext)
        {
            _mongoContext = mongoContext;
            _mongoCollection = _mongoContext.GetCollection<Filme>(typeof(Filme).Name);
            _sqlServercontext = sqlServercontext;
        }
    }
}
