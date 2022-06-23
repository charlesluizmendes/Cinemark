using Cinemark.Domain.Interfaces.Services;
using Cinemark.Domain.Models;
using Cinemark.Infrastructure.Identity.Services;
using Cinemark.Infrastructure.Identity.Services.Option;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Infrastructure.Identity
{
    public class TokenServiceTest
    {
        [Fact]
        public async void CreateTokenAsync()
        {                                  
            var tokenService = new Mock<ITokenService>();
            tokenService.Setup(x => x.CreateTokenAsync(It.IsAny<Usuario>()))
                .ReturnsAsync(new Token() 
                {
                    AccessKey = "eYmcpwojevpoglplapqkdpoqwkwopejmfvpomwevfwvwerv",
                    ValidTo = "2023"
                });

            var usuario = new Usuario()
            {
                Id = 1,
                Nome = "Teste",
                Email = "teste@teste.com",
                Senha = "123",
                DataCriacao = new DateTime(2022, 06, 23)
            };

            var result = await tokenService.Object.CreateTokenAsync(usuario);

            result.AccessKey.Should().Be("eYmcpwojevpoglplapqkdpoqwkwopejmfvpomwevfwvwerv");
            result.ValidTo.Should().Be("2023");
        }        
    }
}
