using Cinemark.Application.Dto;
using FluentAssertions;
using Xunit;

namespace Cinemark.Unit.Tests.Application.Dto
{
    public class CreateFilmeDtoTest
    {
        [Fact]
        public void CreateFilmeDto()
        {
            var request = new CreateFilmeDto()
            {
                Nome = "E o Vento Levou",
                Categoria = "Drama",
                FaixaEtaria = 12,
                DataLancamento = new DateTime(1971, 10, 3)
            };

            request.Nome.Should().Be("E o Vento Levou");
            request.Categoria.Should().Be("Drama");
            request.FaixaEtaria.Should().Be(12);
            request.DataLancamento.Should().Be(new DateTime(1971, 10, 3));
        }
    }
}
