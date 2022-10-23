using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Infrastructure.Data.EventBus
{
    public class FilmeCreateEventBusTest
    {
        [Fact]
        public void PublisherAsync()
        {
            var filme = new Filme()
            {
                Id = 1,
                Nome = "E o Vento Levou",
                Categoria = "Drama",
                FaixaEtaria = 12,
                DataLancamento = new DateTime(1971, 10, 3)
            };

            var filmeCreateEventBus = new Mock<IFilmeCreateEventBus>();
            filmeCreateEventBus.Setup(x => x.PublisherAsync(It.IsAny<Filme>()))
                .Returns(Task.CompletedTask);            

            var result = filmeCreateEventBus.Object.PublisherAsync(filme);

            result.IsCompletedSuccessfully.Should().BeTrue();
        }

        [Fact]
        public void SubscriberAsync()
        {
            var filme = new Filme()
            {
                Id = 1,
                Nome = "E o Vento Levou",
                Categoria = "Drama",
                FaixaEtaria = 12,
                DataLancamento = new DateTime(1971, 10, 3)
            };

            var filmeCreateEventBus = new Mock<IFilmeCreateEventBus>();
            filmeCreateEventBus.Setup(x => x.SubscriberAsync())
                .ReturnsAsync(filme);

            var result = filmeCreateEventBus.Object.SubscriberAsync();

            result.IsCompletedSuccessfully.Should().BeTrue();
        }

        [Fact]
        public void HandleMessageAsync()
        {
            var filme = new Filme()
            {
                Id = 1,
                Nome = "E o Vento Levou",
                Categoria = "Drama",
                FaixaEtaria = 12,
                DataLancamento = new DateTime(1971, 10, 3)
            };

            var filmeCreateEventBus = new Mock<IFilmeCreateEventBus>();
            filmeCreateEventBus.Setup(x => x.HandleMessageAsync())
                .Returns(Task.CompletedTask);

            var result = filmeCreateEventBus.Object.PublisherAsync(filme);

            result.IsCompletedSuccessfully.Should().BeTrue();
        }
    }
}
