using Cinemark.Domain.Models.Commom;
using FluentAssertions;
using System.Net;
using Xunit;

namespace Cinemark.Unit.Tests.Domain.Models.Commom
{    
    public class HttpResultTest
    {
        [Fact]
        public void HttpResult()
        {
            var result = new HttpResult()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "OK"
            };

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Message.Should().Be("OK");
        }

        [Fact]
        public void HttpResultObject()
        {
            var entity = new object
            {
            };

            var result = new HttpResult<object>()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "OK",
                Data = entity
            };

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Message.Should().Be("OK");
            result.Data.Should().Be(entity);
        }
    }
}
