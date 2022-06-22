using Cinemark.Domain.Entities;
using Cinemark.Infrastructure.Data.Mapping.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Cinemark.Infrastructure.Data.Context.SqlServer
{
    public class CinemarkContext : DbContext, IDisposable
    {
        public CinemarkContext(DbContextOptions<CinemarkContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Filme> Filme { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Filme>(new FilmeMap().Configure);
        }
    }
}
