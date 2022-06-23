using Cinemark.Domain.Models;
using FluentAssertions;
using Xunit;

namespace Cinemark.Unit.Tests.Domain.Models
{
    public class TokenTest
    {
        [Fact]
        public void Token()
        {
            var entity = new Token()
            {
                AccessKey = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJjaGFybGVzbHVpem1lbmRlc0BnbWFpbC5jb20iLCJleHAiOjE2NTYwMDA5NTIsImlzcyI6ImNoYXJsZXMubWVuZGVzIiwiYXVkIjoiY2hhcmxlcy5tZW5kZXMifQ.-EOVcaUbT1l5ya6cIGkd4HUMd8TfW9_jebB5mWzMgkw",
                ValidTo = "23/06/2022 16:15:52"
            };

            entity.AccessKey.Should().Be("Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJjaGFybGVzbHVpem1lbmRlc0BnbWFpbC5jb20iLCJleHAiOjE2NTYwMDA5NTIsImlzcyI6ImNoYXJsZXMubWVuZGVzIiwiYXVkIjoiY2hhcmxlcy5tZW5kZXMifQ.-EOVcaUbT1l5ya6cIGkd4HUMd8TfW9_jebB5mWzMgkw");
            entity.ValidTo.Should().Be("23/06/2022 16:15:52");
        }
    }
}
