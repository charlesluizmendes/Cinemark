namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface IEventBus<T> where T : class
    {
        Task SendMessageAsync(T entity);
        Task ReadMessgaesAsync();
        void Dispose();
    }
}
