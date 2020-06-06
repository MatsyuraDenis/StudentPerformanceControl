using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataCore.Repository.Impl
{
    public class Repository : IRepository
    {
        #region Dependecies

        private readonly DbContext _dbContext;

        #endregion

        #region ctor

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion
        
        #region Implementation

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public void Add<T>(T entity) where T : class
        {
            _dbContext.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _dbContext.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _dbContext.Remove(entity);
        }

        public void DeleteRange<T>(T entity) where T : IEnumerable
        {
            _dbContext.RemoveRange(entity);
        }

        public Task SaveContextAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        #endregion
    }
}