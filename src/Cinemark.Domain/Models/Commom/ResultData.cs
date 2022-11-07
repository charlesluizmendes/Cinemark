namespace Cinemark.Domain.Models.Commom
{
    public class ResultData
    {
        public ResultData(bool success)
        {
            Success = success;
        }

        public bool Success { get; protected set; }
        public string? Message { get; set; }
    }

    public class ResultData<T> : ResultData
    {
        public ResultData(bool success)
            : base(success)
        {
            Success = success;
        }

        public T? Data { get; protected set; }
    }
}
