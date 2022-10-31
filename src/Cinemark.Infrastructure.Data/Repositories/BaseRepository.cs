using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Cinemark.Infrastructure.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly MongoContext _mongoContext;
        private IMongoCollection<T> _mongoCollection;
        private readonly SqlServerContext _sqlServerContext;

        public BaseRepository(MongoContext mongoContext,
            SqlServerContext sqlServerContext)
        {
            _mongoContext = mongoContext;
            _mongoCollection = _mongoContext.GetCollection<T>(typeof(T).Name);
            _sqlServerContext = sqlServerContext;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _mongoCollection.Find(Builders<T>.Filter.Empty).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            try
            {
                return await _mongoCollection.Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            try
            {
                await _sqlServerContext.Set<T>().AddAsync(entity);
                await _sqlServerContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            try
            {
                _sqlServerContext.Update(entity);
                await _sqlServerContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<T> DeleteAsync(T entity)
        {
            try
            {
                _sqlServerContext.Set<T>().Remove(entity);
                await _sqlServerContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Dispose

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _sqlServerContext.Dispose();                    
                }
            }

            this.disposed = true;
        }        

        #endregion
    }
}