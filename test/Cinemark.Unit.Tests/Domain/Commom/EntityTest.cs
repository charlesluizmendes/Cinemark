using Cinemark.Domain.Commom;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Domain.Commom
{
    public class EntityTest
    {
        [Fact]
        public void Entity()
        {
            var entity = new Mock<Entity>();
            entity.Setup(x => x.Id).Returns(new Guid("30eb581a-4373-4a49-93a3-6fba8aae2044"));

            entity.Object.Id.Should().Be(new Guid("30eb581a-4373-4a49-93a3-6fba8aae2044"));
        }
    }
}
