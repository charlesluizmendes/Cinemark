namespace Cinemark.Domain.Models.Commom
{
    public class ErrorData<T> : ResultData<T>
    {
        public ErrorData()
            : base(false)
        {
        }

        public ErrorData(string message)
            : base(false)
        {
            Message = message;
        }
    }
}
