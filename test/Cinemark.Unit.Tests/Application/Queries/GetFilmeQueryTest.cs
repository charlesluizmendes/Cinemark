using Cinemark.Application.Queries;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Application.Queries
{
    public class GetFilmeQueryTest
    {
        [Fact]
        public void GetFilmeQuery()
        {
            var getFilmeQuery = new Mock<GetFilmeQuery>();

            getFilmeQuery.Should().NotBeNull();
        }
    }
}
