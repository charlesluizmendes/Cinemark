using Cinemark.Domain.Models.Commom;

namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface IBaseEventBus<T> where T : class
    {
        void CreateConnect();
        void CreateQueue(string queueName);
        Task PublisherAsync(string queueName, T entity);
        Task SubscriberAsync(string queueName, Func<T, CancellationToken, Task<bool>> entity);
        void Dispose();
    }
}
