using Cinemark.Domain.Commom;
using Microsoft.AspNetCore.Mvc;

namespace Cinemark.Service.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected ActionResult CustomResponse()
        {
            return Ok(new ResultData(true, "OK"));
        }

        protected ActionResult CustomResponse(object result)
        {
            return Ok(new ResultData<object>(true, "OK", result));
        }
    }
}