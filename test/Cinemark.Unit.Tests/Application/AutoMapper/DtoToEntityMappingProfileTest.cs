using AutoMapper;
using Cinemark.Application.AutoMapper;
using Cinemark.Application.Dto;
using Cinemark.Domain.Contracts.Commands;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Application.AutoMapper
{
    public class DtoToEntityMappingProfileTest
    {       
        [Fact]
        public void CreateFilmeDto()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DtoToEntityMappingProfile>());
            var mapper = config.CreateMapper();

            var filme = new CreateFilmeDto()
            {
                Nome = "E o Vento Levou",
                Categoria = "Drama",
                FaixaEtaria = 12,
                DataLancamento = new DateTime(1971, 10, 3)
            };

            var result = mapper.Map<CreateFilmeDto, CreateFilmeCommand>(filme);

            result.Nome.Should().Be("E o Vento Levou");
            result.Categoria.Should().Be("Drama");
            result.FaixaEtaria.Should().Be(12);
            result.DataLancamento.Should().Be(new DateTime(1971, 10, 3));
        }
    }
}
