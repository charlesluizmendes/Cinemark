using Cinemark.Domain.Interfaces.Services;
using Cinemark.Domain.Models;
using FluentAssertions;
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
                    AccessKey = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJjaGFybGVzbHVpem1lbmRlc0BnbWFpbC5jb20iLCJleHAiOjE2NTYwMDA5NTIsImlzcyI6ImNoYXJsZXMubWVuZGVzIiwiYXVkIjoiY2hhcmxlcy5tZW5kZXMifQ.-EOVcaUbT1l5ya6cIGkd4HUMd8TfW9_jebB5mWzMgkw",
                    ValidTo = "23/06/2022 16:15:52"
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

            result.AccessKey.Should().Be("Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJjaGFybGVzbHVpem1lbmRlc0BnbWFpbC5jb20iLCJleHAiOjE2NTYwMDA5NTIsImlzcyI6ImNoYXJsZXMubWVuZGVzIiwiYXVkIjoiY2hhcmxlcy5tZW5kZXMifQ.-EOVcaUbT1l5ya6cIGkd4HUMd8TfW9_jebB5mWzMgkw");
            result.ValidTo.Should().Be("23/06/2022 16:15:52");
        }        
    }
}
