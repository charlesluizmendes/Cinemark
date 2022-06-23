using Cinemark.Domain.Models.Commom;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;

namespace Cinemark.Service.Api.Controllers
{    
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        public override OkObjectResult Ok([ActionResultObjectValue] object? value)
        {
            var result = new OkObjectResult(new HttpResult<object>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "OK",
                Data = value
            });

            return result;
        }

        public override NotFoundObjectResult NotFound([ActionResultObjectValue] object? value)
        {
            var result = new NotFoundObjectResult(new HttpResult
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = value.ToString()                
            });

            return result;
        }

        public override BadRequestObjectResult BadRequest([ActionResultObjectValue] object? error)
        {
            var result = new BadRequestObjectResult(new HttpResult
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = error.ToString()               
            });

            return result;
        }
    }
}