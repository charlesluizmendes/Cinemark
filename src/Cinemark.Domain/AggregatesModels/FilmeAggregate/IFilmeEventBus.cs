namespace Cinemark.Domain.AggregatesModels.FilmeAggregate
{
    public interface IFilmeEventBus
    {
        void CreateQueue(string queueName);
        Task PublisherAsync(string queueName, Filme filme);
        Task SubscriberAsync(string queueName, Func<Filme, CancellationToken, Task<bool>> filme);
        void Dispose();
    }
}
