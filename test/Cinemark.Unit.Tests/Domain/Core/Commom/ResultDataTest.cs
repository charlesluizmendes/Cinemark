using Cinemark.Domain.Core.Commom;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Domain.Core.Commom
{
    public class ResultDataTest
    {
        [Fact]
        public void ResultData()
        {
            var result = new Mock<ResultData>(true, "OK");

            result.Object.Success.Should().Be(true);
            result.Object.Message.Should().Be("OK");
        }
    }
}
