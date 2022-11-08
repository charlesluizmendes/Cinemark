using Cinemark.Domain.Models.Commom;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Cinemark.Application.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                await HandlerUnauthorizedAsync(context);               
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandlerUnauthorizedAsync(HttpContext context)
        {
            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                var result = new ResultData(false)
                {
                    Message = "Unauthorized"                    
                };

                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                await context.Response.WriteAsJsonAsync(result);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            var result = new ResultData(false)
            {
                Message = ex.Message          
            };

            await context.Response.WriteAsJsonAsync(result);
        }
    }
}
