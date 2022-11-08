using Cinemark.Domain.Models.Commom;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;

namespace Cinemark.Service.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        public static ActionResult HttpResult<T>(ResultData<T> result) where T : class
        {
            if (result.Success)
            {
                return new OkObjectResult(result);
            }

            return new BadRequestObjectResult(result);
        }
    }
}