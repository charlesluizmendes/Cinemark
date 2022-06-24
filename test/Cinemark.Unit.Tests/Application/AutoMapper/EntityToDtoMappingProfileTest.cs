using AutoMapper;
using Cinemark.Application.AutoMapper;
using Cinemark.Application.Dto;
using Cinemark.Domain.Models;
using FluentAssertions;
using Xunit;

namespace Cinemark.Unit.Tests.Application.AutoMapper
{
    public class EntityToDtoMappingProfileTest
    {
        [Fact]
        public void Filme()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<EntityToDtoMappingProfile>());
            var mapper = config.CreateMapper();

            var entity = new Filme()
            {
                Id = 1,
                Nome = "E o Vento Levou",
                Categoria = "Drama",
                FaixaEtaria = 12,
                DataLancamento = new DateTime(1971, 10, 3)
            };

            var result = mapper.Map<Filme, FilmeDto>(entity);

            result.Id.Should().Be(1);
            result.Nome.Should().Be("E o Vento Levou");
            result.Categoria.Should().Be("Drama");
            result.FaixaEtaria.Should().Be(12);
            result.DataLancamento.Should().Be(new DateTime(1971, 10, 3));
        }
    }
}
