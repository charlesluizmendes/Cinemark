using Cinemark.Domain.AggregatesModels.UsuarioAggregate;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Domain.AggregateModels.UsuarioAggregate
{
    public class UsuarioTest
    {
        [Fact]
        public void Usuario()
        {
            var entity = new Mock<Usuario>("Teste", "teste@teste.com", "12345", new DateTime(2022, 06, 23));

            entity.Object.Nome.Should().Be("Teste");
            entity.Object.Email.Should().Be("teste@teste.com");
            entity.Object.Senha.Should().Be("12345");
            entity.Object.DataCriacao.Should().Be(new DateTime(2022, 06, 23));
        }
    }
}
