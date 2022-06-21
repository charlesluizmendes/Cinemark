using Cinemark.Domain.Entities;
using Cinemark.Infrastructure.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Cinemark.Infrastructure.Data.Context
{
    public class CinemarkSqlServerContext : DbContext, IDisposable
    {
        public CinemarkSqlServerContext(DbContextOptions<CinemarkSqlServerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Filme> Filme { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Filme>(new FilmeSqlServerMap().Configure);
        }
    }
}
