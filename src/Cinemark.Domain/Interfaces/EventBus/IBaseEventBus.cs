namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface IBaseEventBus<T> where T : class
    {
        Task PublisherAsync(T entity);
        Task<T?> SubscriberAsync();
        void Dispose();
    }
}
