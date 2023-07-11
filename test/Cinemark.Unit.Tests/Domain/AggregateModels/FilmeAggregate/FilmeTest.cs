using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Domain.AggregateModels.FilmeAggregate
{
    public class FilmeTest
    {
        [Fact]
        public void Filme()
        {
            var entity = new Mock<Filme>(new Guid("30eb581a-4373-4a49-93a3-6fba8aae2044"), "E o Vento Levou", "Drama", 12, new DateTime(1971, 10, 3));
            entity.Setup(x => x.Id).Returns(new Guid("30eb581a-4373-4a49-93a3-6fba8aae2044"));

            entity.Object.Id.Should().Be(new Guid("30eb581a-4373-4a49-93a3-6fba8aae2044"));
            entity.Object.Nome.Should().Be("E o Vento Levou");
            entity.Object.Categoria.Should().Be("Drama");
            entity.Object.FaixaEtaria.Should().Be(12);
            entity.Object.DataLancamento.Should().Be(new DateTime(1971, 10, 3));
        }
    }
}
