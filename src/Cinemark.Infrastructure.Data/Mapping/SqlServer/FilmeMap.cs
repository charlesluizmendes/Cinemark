using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinemark.Infrastructure.Data.Mapping.SqlServer
{
    internal class FilmeMap : IEntityTypeConfiguration<Filme>
    {
        public void Configure(EntityTypeBuilder<Filme> builder)
        {
            builder.ToTable("Filme");

            builder.Property(c => c.Id).IsRequired();
            builder.Property(p => p.Nome).IsRequired();
            builder.Property(p => p.Categoria).IsRequired();
            builder.Property(p => p.FaixaEtaria).IsRequired();
            builder.Property(p => p.DataLancamento).IsRequired();
        }
    }
}
