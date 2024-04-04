using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace JsLocalization.DAL
{
    public class BaseEntity
    {
        //empty Core! Add if you want, do not mess up!
        //
    }

    public interface IRepository<T> where T : BaseEntity
    {
        IDbContextTransaction BeginTransaction();
        IEnumerable<T> GetAll();
        IQueryable<T> GetAsIQueryable();
        T Get(object id);
        int InsertScopeIdentity(T entity);
        T Insert(T entity);
        void InsertList(List<T> entities);
        bool Update(T entity);
        T UpdateAndReturnEntity(T entity);
        void UpdateList(List<T> entities);
        void UpdateIList(IList<T> entities);
        Task<int> UpdateWithAttach(T entity);
        void Delete(T entity);
        void DeleteList(IEnumerable<T> entities);
        Task<int> DeleteWithResult(T entity);
        //async methods
        Task<T> GetAsync(object id);
        Task DeleteAsync(T entity);
        Task InsertAsync(T entity);
        Task<int> InsertAsyncWithResult(T entity);
        Task<int> InsertWithAttach(T entity);
        Task UpdateAsync(T entity);
        Task<int> UpdateWithResultAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(int take, int skip, Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync(int take, int skip);
        Task<bool> Any(Expression<Func<T, bool>> predicate);
        Task<int> CountAll();
        Task<int> CountWhere(Expression<Func<T, bool>> predicate);        
        int AddRange(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);
        bool Contains(Expression<Func<T, bool>> predicate);
        int Count(Expression<Func<T, bool>> predicate);
        Task<int> RemoveRangeAsync(IEnumerable<T> entities);
        Task<int> UpdateRangeAsync(IEnumerable<T> entities);        
    }
}
