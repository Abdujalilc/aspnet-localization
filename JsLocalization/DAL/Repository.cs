using Microsoft.EntityFrameworkCore;
using JsLocalization.Models;
using System.Linq.Expressions;

namespace JsLocalization.DAL
{
    public static class ExtensionsInc 
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }
    }
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly LanguageDbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        public Repository(LanguageDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }        
        public IQueryable<T> GetAsIQueryable()
        {
            return entities;
        }
        public T Get(object id)
        {
            return entities.Find(id);
        }        
        public T Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                entities.Add(entity);
                context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }       
        public bool Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }        
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }              
        public int AddRange(IEnumerable<T> entities)
        {
            try
            {
                context.Set<T>().AddRange(entities);
                return context.SaveChanges();

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int Count(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate).Count();
        }
        
    }
}
