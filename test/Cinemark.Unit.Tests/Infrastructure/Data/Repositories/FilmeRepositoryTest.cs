using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Infrastructure.Data.Repositories
{
    public class FilmeRepositoryTest
    {
        [Fact]
        public async void InsertAsync()
        {
            var filme = new Filme()
            {
                Id = 1,
                Nome = "E o Vento Levou",
                Categoria = "Drama",
                FaixaEtaria = 12,
                DataLancamento = new DateTime(1971, 10, 3)
            };

            var filmeRepository = new Mock<IFilmeRepository>();
            filmeRepository.Setup(x => x.InsertAsync(It.IsAny<Filme>()))
                .ReturnsAsync(new SuccessData<Filme>(filme));            
            
            var result = await filmeRepository.Object.InsertAsync(filme);

            result.Data.Id.Should().Be(1);
            result.Data.Nome.Should().Be("E o Vento Levou");
            result.Data.Categoria.Should().Be("Drama");
            result.Data.FaixaEtaria.Should().Be(12);
            result.Data.DataLancamento.Should().Be(new DateTime(1971, 10, 3));
        }
    }
}
