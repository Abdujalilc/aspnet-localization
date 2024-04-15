using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace JsLocalizeText.DAL
{
    public class BaseEntity
    {
        //empty Core! Add if you want, do not mess up!
        //
    }

    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAsIQueryable();
        T Get(object id);
        T Insert(T entity);
        bool Update(T entity);
        void Delete(T entity);       
        int AddRange(IEnumerable<T> entities);
        int Count(Expression<Func<T, bool>> predicate);      
    }
}
