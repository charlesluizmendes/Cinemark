using Cinemark.Domain.Entities;
using Cinemark.Infrastructure.Data.Mapping.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Cinemark.Infrastructure.Data.Context
{
    public class SqlServerContext : DbContext, IDisposable
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options)
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
