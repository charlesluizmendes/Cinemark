using Cinemark.Application.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Application.MIddleware
{
    public class ExceptionMiddlewareTest
    {
        [Fact]
        public async void ExceptionMiddleware()
        {
            bool isNextDelegateCalled = false;
            Mock<HttpContext> httpContextMock = new();
            Mock<HttpResponse> httpResponseMock = new();
            Func<Task>? callbackMethod = null;

            httpResponseMock.Setup(x => x
                .OnStarting(It.IsAny<Func<Task>>()))
                .Callback<Func<Task>>(m => callbackMethod = m);

            httpContextMock.SetupGet(x => x.Response)
                .Returns(httpResponseMock.Object);

            httpContextMock.SetupGet(x => x.Response.Headers)
                .Returns(new HeaderDictionary());

            var fakeHttpContext = httpContextMock.Object;

            var requestDelegate = new RequestDelegate(async (innerContext) =>
            {
                isNextDelegateCalled = true;

                if (callbackMethod != null)
                {
                    await callbackMethod.Invoke();
                }
                else
                {
                    await Task.CompletedTask;
                }
            });

            var logger = new Mock<ILogger<ExceptionMiddleware>>();

            var middelware = new ExceptionMiddleware(requestDelegate, logger.Object);

            await middelware.InvokeAsync(fakeHttpContext);

            Assert.True(isNextDelegateCalled);
        }
    }
}
