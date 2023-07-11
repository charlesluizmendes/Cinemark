using Cinemark.Domain.AggregatesModels.UsuarioAggregate;
using Cinemark.Domain.Core.Commom;
using Cinemark.Infrastructure.Data.Context;
using MongoDB.Driver;

namespace Cinemark.Infrastructure.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MongoContext _mongoContext;
        private readonly SqlServerContext _sqlServerContext;
        public IUnitOfWork UnitOfWork => _sqlServerContext;

        public UsuarioRepository(MongoContext mongoContext, SqlServerContext sqlServerContext)
        {
            _mongoContext = mongoContext ?? throw new ArgumentNullException(nameof(mongoContext));
            _sqlServerContext = sqlServerContext ?? throw new ArgumentNullException(nameof(sqlServerContext));
        }

        public async Task<Usuario> GetUsuarioByEmailAndSenhaAsync(string email, string senha)
        {
            return await _mongoContext.GetCollection<Usuario>().Find(x => x.Email == email && x.Senha == senha).FirstOrDefaultAsync();
        }
    }
}
