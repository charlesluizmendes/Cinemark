using Cinemark.Domain.Models;
using FluentAssertions;
using Xunit;

namespace Cinemark.Unit.Tests.Domain.Models
{
    public class FilmeTest
    {
        [Fact]
        public void Filme()
        {
            var entity = new Filme()
            {
                Id = 1,
                Nome = "E o Vento Levou",
                Categoria = "Drama",
                FaixaEtaria = 12,
                DataLancamento = new DateTime(1971, 10, 3)
            };

            entity.Id.Should().Be(1);
            entity.Nome.Should().Be("E o Vento Levou");
            entity.Categoria.Should().Be("Drama");
            entity.FaixaEtaria.Should().Be(12);
            entity.DataLancamento.Should().Be(new DateTime(1971, 10, 3));
        }
    }
}
