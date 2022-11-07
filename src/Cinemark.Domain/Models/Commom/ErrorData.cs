namespace Cinemark.Domain.Models.Commom
{
    public class ErrorData<T> : ResultData<T>
    {      
        public ErrorData(string message)
            : base(false)
        {
            Message = message;
        }
    }
}
