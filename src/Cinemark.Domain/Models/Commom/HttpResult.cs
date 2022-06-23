using System.Net;

namespace Cinemark.Domain.Models.Commom
{
    public class HttpResult
    {
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }

    public class HttpResult<T> : HttpResult where T : class
    {
        public T? Data { get; set; }
    }
}
