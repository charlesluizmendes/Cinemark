using Cinemark.Application.QueryHandlers;
using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Cinemark.Domain.Contracts.Queries;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Application.Queries
{
    public class GetFilmeQueryHandlerTest
    {
        [Fact]
        public async void GetFilmeQueryHandler()
        {
            var filmes = new List<Filme>()
            {
                new Mock<Filme>(new Guid("30eb581a-4373-4a49-93a3-6fba8aae2044"), "E o Vento Levou", "Drama", 12, new DateTime(1971, 10, 3)).Object
            };

            var filmeRepository = new Mock<IFilmeRepository>();
            filmeRepository.Setup(x => x.GetAllAsync())
                .ReturnsAsync(filmes);

            var query = new Mock<GetFilmeQuery>();
            var handler = new Mock<GetFilmeQueryHandler>(filmeRepository.Object);

            var results = await handler.Object.Handle(query.Object, new CancellationToken());

            foreach (var result in results)
            {
                if (result.Id.Equals(new Guid("30eb581a-4373-4a49-93a3-6fba8aae2044")))

                    result.Id.Should().Be(new Guid("30eb581a-4373-4a49-93a3-6fba8aae2044"));
                result.Nome.Should().Be("E o Vento Levou");
                result.Categoria.Should().Be("Drama");
                result.FaixaEtaria.Should().Be(12);
                result.DataLancamento.Should().Be(new DateTime(1971, 10, 3));
            }
        }
    }
}
