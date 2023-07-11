using Cinemark.Domain.Commom;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Domain.Commom
{
    public class ValueObjectTest
    {
        [Fact]
        public void ValueObject()
        {
            var valueObject = new Mock<ValueObject>();

            valueObject.Object.Should().NotBeNull();
        }
    }
}
