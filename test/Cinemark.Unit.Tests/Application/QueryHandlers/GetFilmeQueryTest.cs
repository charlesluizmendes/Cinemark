using Cinemark.Domain.Contracts.Queries;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Application.QueryHandlers
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
