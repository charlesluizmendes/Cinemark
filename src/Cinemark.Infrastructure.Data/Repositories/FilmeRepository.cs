using Cinemark.Domain.Interfaces.EventBus;
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

        private readonly IFilmeEventBus _filmeEventBus;

        public FilmeRepository(MongoContext mongoContext,
            SqlServerContext sqlServercontext,
            IFilmeEventBus filmeEventBus) 
            : base(mongoContext, sqlServercontext, filmeEventBus)
        {
            _mongoContext = mongoContext;
            _mongoCollection = _mongoContext.GetCollection<Filme>(typeof(Filme).Name);
            _sqlServercontext = sqlServercontext;

            _filmeEventBus = filmeEventBus;
        }
    }
}
