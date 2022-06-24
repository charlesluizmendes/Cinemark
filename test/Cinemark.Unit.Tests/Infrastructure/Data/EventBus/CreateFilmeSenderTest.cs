using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Infrastructure.Data.EventBus
{
    public class CreateFilmeSenderTest
    {
        [Fact]
        public void SendMessageAsync()
        {
            var filme = new Filme()
            {
                Id = 1,
                Nome = "E o Vento Levou",
                Categoria = "Drama",
                FaixaEtaria = 12,
                DataLancamento = new DateTime(1971, 10, 3)
            };

            var createFilmeSender = new Mock<ICreateFilmeSender>();
            createFilmeSender.Setup(x => x.SendMessageAsync(It.IsAny<Filme>()))
                .Returns(Task.CompletedTask);            

            var result = createFilmeSender.Object.SendMessageAsync(filme);

            result.IsCompletedSuccessfully.Should().BeTrue();
        }
    }
}
