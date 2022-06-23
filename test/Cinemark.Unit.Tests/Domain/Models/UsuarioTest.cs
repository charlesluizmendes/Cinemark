using Cinemark.Domain.Models;
using FluentAssertions;
using Xunit;

namespace Cinemark.Unit.Tests.Domain.Models
{
    public class UsuarioTest
    {
        [Fact]
        public void Usuario()
        {
            var entity = new Usuario()
            {
                Id = 1,
                Nome = "Teste",
                Email = "teste@teste.com",
                Senha = "123",
                DataCriacao = new DateTime(2022, 06, 23)
            };

            entity.Id.Should().Be(1);
            entity.Nome.Should().Be("Teste");
            entity.Email.Should().Be("teste@teste.com");
            entity.Senha.Should().Be("123");
            entity.DataCriacao.Should().Be(new DateTime(2022, 06, 23));
        }
    }
}
