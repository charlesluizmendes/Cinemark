using Cinemark.Application.Events.Queries;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Application.Events.Queries
{
    public class GetFilmeQueryHandlerTest
    {
        [Fact]
        public async void GetFilmeQueryHandler()
        {
            var filmes = new List<Filme>()
            {
                new Filme()
                {
                    Id = 1,
                    Nome = "E o Vento Levou",
                    Categoria = "Drama",
                    FaixaEtaria = 12,
                    DataLancamento = new DateTime(1971, 10, 3)
                }
            };

            var filmeRepositoryMock = new Mock<IFilmeRepository>();
            filmeRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(filmes);

            var query = new GetFilmeQuery();
            var handler = new Mock<GetFilmeQueryHandler>(filmeRepositoryMock.Object);

            var results = await handler.Object.Handle(query, new CancellationToken());

            foreach (var result in results)
            {
                if (result.Id.Equals(1))
                    result.Id.Should().Be(1);
                    result.Nome.Should().Be("E o Vento Levou");
                    result.Categoria.Should().Be("Drama");
                    result.FaixaEtaria.Should().Be(12);
                    result.DataLancamento.Should().Be(new DateTime(1971, 10, 3));
            }            
        }
    }
}
