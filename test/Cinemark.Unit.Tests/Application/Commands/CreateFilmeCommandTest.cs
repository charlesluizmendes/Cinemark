using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Cinemark.Domain.Contracts.Commands;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Application.Commands
{
    public class CreateFilmeCommandTest
    {
        [Fact]
        public void CreateFilmeCommand()
        {
            var filme = new Mock<Filme>(new Guid("30eb581a-4373-4a49-93a3-6fba8aae2044"), "E o Vento Levou", "Drama", 12, new DateTime(1971, 10, 3));
            filme.Setup(x => x.Id).Returns(new Guid("30eb581a-4373-4a49-93a3-6fba8aae2044"));

            var createFilmeCommand = new Mock<CreateFilmeCommand>(filme.Object.Nome, filme.Object.Categoria, filme.Object.FaixaEtaria, filme.Object.DataLancamento);

            createFilmeCommand.Object.Nome.Should().Be("E o Vento Levou");
            createFilmeCommand.Object.Categoria.Should().Be("Drama");
            createFilmeCommand.Object.FaixaEtaria.Should().Be(12);
            createFilmeCommand.Object.DataLancamento.Should().Be(new DateTime(1971, 10, 3));
        }
    }
}
