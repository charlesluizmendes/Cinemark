using Cinemark.Application.Dto;
using Cinemark.Application.Validators;
using FluentAssertions;
using Xunit;

namespace Cinemark.Unit.Tests.Application.Validators
{
    public class CreateFilmeDtoValidatorTest
    {
        [Fact]
        public void CreateFilmeDtoValidator()
        {
            var request = new CreateFilmeDto()
            {               
                Nome = "E o Vento Levou",
                Categoria = "Drama",
                FaixaEtaria = 12,
                DataLancamento = new DateTime(1971, 10, 3)
            };

            var validation = new CreateFilmeDtoValidator().Validate(request);

            validation.IsValid.Should().BeTrue();
        }
    }
}
