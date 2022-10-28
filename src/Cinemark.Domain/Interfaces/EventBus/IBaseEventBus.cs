namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface IBaseEventBus<T> where T : class
    {
        void TryConnect();
        void Queue();
        Task PublisherAsync(T entity);
        Task SubscriberAsync();
        Task HandlerMessageAsync(T entity);
        void Dispose();
    }
}
