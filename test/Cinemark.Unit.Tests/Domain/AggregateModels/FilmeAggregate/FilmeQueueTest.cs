using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Domain.AggregateModels.FilmeAggregate
{
    public class FilmeQueueTest
    {
        [Fact]
        public void FilmeQueue()
        {
            var filmeQueue = new Mock<FilmeQueue>(1, "Created");

            filmeQueue.Object.Id.Should().Be(1);
            filmeQueue.Object.Name.Should().Be("Created");
        }
    }
}
