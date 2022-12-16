using Cinemark.Domain.Constants;
using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Infrastructure.Data.EventBus
{
    public class FilmeEventBusTest
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

            var filmeCreateEventBus = new Mock<IFilmeEventBus>();
            filmeCreateEventBus.Setup(x => x.PublisherAsync(typeof(Filme).Name + QueueConstants.Insert, It.IsAny<Filme>()))
                .Returns(Task.CompletedTask);            

            var result = filmeCreateEventBus.Object.PublisherAsync(typeof(Filme).Name + QueueConstants.Insert, filme);

            result.IsCompletedSuccessfully.Should().BeTrue();
        }

        [Fact]
        public void SubscriberAsync()
        {
            var filmeCreateEventBus = new Mock<IFilmeEventBus>();            

            var result = filmeCreateEventBus.Object.SubscriberAsync(typeof(Filme).Name + QueueConstants.Insert, (message, cancellationToken) => 
            {
                return Task.FromResult(true);
            });

            result.IsCompletedSuccessfully.Should().BeTrue();
        }       
    }
}
