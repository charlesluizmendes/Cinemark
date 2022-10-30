using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using Cinemark.Infrastructure.Data.Context;
using MongoDB.Driver;

namespace Cinemark.Infrastructure.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly MongoContext _mongoContext;
        private IMongoCollection<Usuario> _mongoCollection;
        private readonly SqlServerContext _sqlServercontext;
        private readonly IUsuarioEventBus _usuarioEventBus;

        public UsuarioRepository(MongoContext mongoContext,
            SqlServerContext sqlServercontext,
            IUsuarioEventBus usuarioEventBus)
            : base(mongoContext, sqlServercontext, usuarioEventBus)
        {
            _mongoContext = mongoContext;
            _mongoCollection = _mongoContext.GetCollection<Usuario>(typeof(Usuario).Name);
            _sqlServercontext = sqlServercontext;
            _usuarioEventBus = usuarioEventBus;
        }

        public async Task<Usuario> GetUsuarioByEmailAndSenhaAsync(Usuario usuario)
        {
            try
            {
                return await _mongoCollection.Find(x => x.Email == usuario.Email && x.Senha == usuario.Senha).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
