using Cinemark.Domain.Entities;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Infrastructure.Data.Context.Mongo;
using Cinemark.Infrastructure.Data.Context.SqlServer;
using MongoDB.Driver;

namespace Cinemark.Infrastructure.Data.Repositories
{
    public class FilmeRepository : BaseRepository<Filme>, IFilmeRepository
    {
        private readonly Context.Mongo.CinemarkContext _mongoContext;
        private IMongoCollection<Filme> _dbMongoCollection;
        private readonly Context.SqlServer.CinemarkContext _sqlServercontext;

        public FilmeRepository(Context.Mongo.CinemarkContext mongoContext,
            Context.SqlServer.CinemarkContext sqlServercontext) 
            : base(mongoContext, sqlServercontext)
        {
            _mongoContext = mongoContext;
            _dbMongoCollection = _mongoContext.GetCollection<Filme>(typeof(Filme).Name);
            _sqlServercontext = sqlServercontext;
        }
    }
}
