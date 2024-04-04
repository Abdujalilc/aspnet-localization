using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
        public IDbContextTransaction BeginTransaction()
        {
            return context.Database.BeginTransaction();
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }
        public IQueryable<T> GetAsIQueryable()
        {
            return entities;
        }
        public T Get(object id)
        {
            return entities.Find(id);
        }
        public int InsertScopeIdentity(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Add(entity);
            return context.SaveChanges();
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
        public void InsertList(List<T> entities)
        {
            try
            {
                foreach (var item in entities)
                {
                    if (item == null)
                    {
                        throw new ArgumentNullException("entity");
                    }
                    context.Add(item);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        public async Task<int> UpdateWithAttach(T entity)
        {
            try
            {
                context.Attach(entity).State = EntityState.Modified;
                int result = await context.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
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
                //context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public T UpdateAndReturnEntity(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                context.Update(entity);
                context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateList(List<T> entities)
        {
            try
            {

                foreach (var item in entities)
                {
                    if (item == null)
                    {
                        throw new ArgumentNullException("entity");
                    }
                    context.Attach(item);
                    context.Entry(item).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateIList(IList<T> entities)
        {
            try
            {
                context.AttachRange(entities);
                context.UpdateRange(entities);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

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
        public void DeleteList(IEnumerable<T> entities)
        {
            try
            {
                context.RemoveRange(entities);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        public async Task<int> DeleteWithResult(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            int result = await context.SaveChangesAsync();
            return result;
        }
        //async methods

        public async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                await entities.AddAsync(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    throw new Exception(ex.InnerException.Message);
                else
                    throw new Exception(ex.Message);

            }
        }
        public async Task<int> InsertAsyncWithResult(T entity)
        {
            int result = 0;
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                await entities.AddAsync(entity);
                result = await context.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<int> InsertWithAttach(T entity)
        {
            int result = 0;
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                entities.Attach(entity);
                context.Entry(entity).State = EntityState.Added;

                return await context.SaveChangesAsync(); ;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                entities.Remove(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var error = ex;
            }
        }
        public async Task<int> UpdateWithResultAsync(T entity)
        {
            int result = 0;
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var error = ex;
                return result;
            }
        }
        public async Task<T> GetAsync(object id)
        {
            return await entities.FindAsync(id);
        }
        public async Task<bool> Any(Expression<Func<T, bool>> predicate)
        {

            return await entities.AnyAsync(predicate);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await entities.Where(predicate).ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync(int take, int skip)
        {
            return await entities.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(int take, int skip, Expression<Func<T, bool>> predicate)
        {
            var result = await entities.Where(predicate).Skip(skip).Take(take).ToListAsync();
            return result;
        }
        public Task<int> CountAll() => context.Set<T>().CountAsync();

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate)
            => context.Set<T>().CountAsync(predicate);

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
        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            return Count(predicate) > 0 ? true : false;
        }
        public int Count(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate).Count();
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
        }
        public async Task<int> RemoveRangeAsync(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
            return await context.SaveChangesAsync();
        }
        public async Task<int> UpdateRangeAsync(IEnumerable<T> entities)
        {
            context.Set<T>().UpdateRange(entities);
            return await context.SaveChangesAsync();
        }
    }
}
