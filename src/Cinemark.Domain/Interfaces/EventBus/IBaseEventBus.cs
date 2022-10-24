namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface IBaseEventBus<T> where T : class
    {
        Task PublisherAsync(T entity);
        Task SubscriberAsync();
        Task HandleMessageAsync(T entity);
        void Dispose();
    }
}
