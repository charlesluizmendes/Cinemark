namespace Cinemark.Domain.Core.Commom
{
    public class ResultData
    {
        public ResultData(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; protected set; }
        public string Message { get; protected set; }
    }

    public class ResultData<T> : ResultData
    {
        public ResultData(bool success, string message, T data)
            : base(success, message)
        {
            Data = data;
        }
       
        public T Data { get; private set; }
    }
}
