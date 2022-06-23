namespace Cinemark.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        void Dispose();
    }
}
