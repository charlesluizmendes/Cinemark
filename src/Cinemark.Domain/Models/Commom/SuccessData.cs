namespace Cinemark.Domain.Models.Commom
{
    public class SuccessData<T> : ResultData<T>
    {
        public SuccessData(T data)
            : base(true)
        {
            Data = data;
            Message = "OK";
        }
    }
}
