using Cinemark.Application.Events.Commands;
using Cinemark.Domain.Models;
using FluentAssertions;
using Xunit;

namespace Cinemark.Unit.Tests.Application.Events.Commands
{
    public class CreateFilmeCommandTest
    {
        [Fact]
        public void CreateFilmeCommand()
        {
            var filme = new Filme()
            {
                Id = 1,
                Nome = "E o Vento Levou",
                Categoria = "Drama",
                FaixaEtaria = 12,
                DataLancamento = new DateTime(1971, 10, 3)
            };

            var createFilmeCommand = new CreateFilmeCommand()
            {
                Filme = filme
            };

            createFilmeCommand.Filme.Id.Should().Be(1);
            createFilmeCommand.Filme.Nome.Should().Be("E o Vento Levou");
            createFilmeCommand.Filme.Categoria.Should().Be("Drama");
            createFilmeCommand.Filme.FaixaEtaria.Should().Be(12);
            createFilmeCommand.Filme.DataLancamento.Should().Be(new DateTime(1971, 10, 3));
        }
    }
}
