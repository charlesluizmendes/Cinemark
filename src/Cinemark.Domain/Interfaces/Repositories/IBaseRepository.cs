using Cinemark.Domain.Models.Commom;

namespace Cinemark.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<ResultData<IEnumerable<T>>> GetAllAsync();
        Task<ResultData<T>> GetByIdAsync(object id);
        Task<ResultData<T>> InsertAsync(T entity);
        Task<ResultData<T>> UpdateAsync(T entity);
        Task<ResultData<T>> DeleteAsync(T entity);
        void Dispose();
    }
}
