using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Cinemark.Domain.AggregatesModels.UsuarioAggregate;
using Cinemark.Domain.Commom;
using Cinemark.Infrastructure.Data.Mapping.SqlServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinemark.Infrastructure.Data.Context
{
    public class SqlServerContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public SqlServerContext(DbContextOptions<SqlServerContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public virtual DbSet<Filme> Filme { get; set; } = null!;
        public virtual DbSet<Usuario> Usuario { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Filme>(new FilmeMap().Configure);
            modelBuilder.Entity<Usuario>(new UsuarioMap().Configure);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

    internal static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, SqlServerContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
