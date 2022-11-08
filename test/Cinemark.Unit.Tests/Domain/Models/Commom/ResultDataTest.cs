using Cinemark.Domain.Models.Commom;
using FluentAssertions;
using Xunit;

namespace Cinemark.Unit.Tests.Domain.Models.Commom
{
    public class ResultDataTest
    {
        [Fact]
        public void ResultData()
        {
            var result = new ResultData<object>(true)
            {
                Message = "OK"
            };

            result.Success.Should().Be(true);
            result.Message.Should().Be("OK");
        }
    }
}
