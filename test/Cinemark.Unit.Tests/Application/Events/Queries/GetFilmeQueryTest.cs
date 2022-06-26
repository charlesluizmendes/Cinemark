using Cinemark.Application.Events.Queries;
using FluentAssertions;
using Xunit;

namespace Cinemark.Unit.Tests.Application.Events.Queries
{
    public class GetFilmeQueryTest
    {
        [Fact]
        public void GetFilmeQuery()
        {
            var getFilmeQuery = new GetFilmeQuery()
            {                
            };

            getFilmeQuery.Should().NotBeNull();
        }
    }
}
