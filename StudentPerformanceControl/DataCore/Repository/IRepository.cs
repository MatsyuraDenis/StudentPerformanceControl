using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace DataCore.Repository
{
    public interface IRepository : IDisposable
    {
        IQueryable<T> GetAll<T>() where T : class;
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteRange<T>(T entity) where T : IEnumerable;
        Task SaveContextAsync();
    }
}