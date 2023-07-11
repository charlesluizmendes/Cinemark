using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Infrastructure.EventBus
{
    public class FilmeEventBusTest
    {
        [Fact]
        public void PublisherAsync()
        {
            var filme = new Mock<Filme>(new Guid("30eb581a-4373-4a49-93a3-6fba8aae2044"), "E o Vento Levou", "Drama", 12, new DateTime(1971, 10, 3));
            filme.Setup(x => x.Id).Returns(new Guid("30eb581a-4373-4a49-93a3-6fba8aae2044"));

            var filmeCreateEventBus = new Mock<IFilmeEventBus>();
            filmeCreateEventBus.Setup(x => x.PublisherAsync(typeof(Filme).Name + FilmeQueue.Created.Name, It.IsAny<Filme>()))
                .Returns(Task.CompletedTask);

            var result = filmeCreateEventBus.Object.PublisherAsync(typeof(Filme).Name + FilmeQueue.Created.Name, filme.Object);

            result.IsCompletedSuccessfully.Should().BeTrue();
        }

        [Fact]
        public void SubscriberAsync()
        {
            var filmeCreateEventBus = new Mock<IFilmeEventBus>();

            var result = filmeCreateEventBus.Object.SubscriberAsync(typeof(Filme).Name + FilmeQueue.Created.Name, (message, cancellationToken) =>
            {
                return Task.FromResult(true);
            });

            result.IsCompletedSuccessfully.Should().BeTrue();
        }
    }
}
