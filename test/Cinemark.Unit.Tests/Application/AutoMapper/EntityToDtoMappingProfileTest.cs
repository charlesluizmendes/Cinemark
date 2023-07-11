using AutoMapper;
using Cinemark.Application.AutoMapper;
using Cinemark.Application.Dto;
using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Application.AutoMapper
{
    public class EntityToDtoMappingProfileTest
    {
        [Fact]
        public void CreateClientCommand()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<EntityToDtoMappingProfile>());
            var mapper = config.CreateMapper();

            var request = new Mock<Filme>(new Guid("30eb581a-4373-4a49-93a3-6fba8aae2044"), "E o Vento Levou", "Drama", 12, new DateTime(1971, 10, 3));
            request.Setup(x => x.Id).Returns(new Guid("30eb581a-4373-4a49-93a3-6fba8aae2044"));

            var result = mapper.Map<Filme, FilmeDto>(request.Object);

            result.Id.Should().Be(new Guid("30eb581a-4373-4a49-93a3-6fba8aae2044"));
            result.Nome.Should().Be("E o Vento Levou");
            result.Categoria.Should().Be("Drama");
            result.FaixaEtaria.Should().Be(12);
            result.DataLancamento.Should().Be(new DateTime(1971, 10, 3));
        }
    } 
}
