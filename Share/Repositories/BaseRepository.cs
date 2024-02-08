using Microsoft.EntityFrameworkCore;
using Share.Contexts;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Share.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;

        protected BaseRepository(DataContext context)
        {
            _context = context;
        }

        //Virtual gör så att koden går att modifiera. _context.Set<TEntity> gör att värden sätts för just den
        //bestämda entiteten .Add lägger till Entiteten.
        public virtual async Task<TEntity> Create(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex) { Debug.WriteLine("BaseResCreate" + ex.Message);
            return null!;
            }
        }

        public virtual async Task<List<TEntity>> GetAll() 
        {
            try
            {
                var result = await _context.Set<TEntity>().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("BaseResGetAll" + ex.Message);
                return null!;
            }
        }

        public virtual async Task<TEntity> GetOne(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);

                if (result != null)
                {
                    return result;
                }
                return null!;
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("BaseResGetOne" + ex.Message);
                return null!;
            }
        }

        public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            try 
            {
                var result = _context.Set<TEntity>().Any(predicate);
                return result;
            }
            catch (Exception ex) { Debug.WriteLine("ExistsBaseRes" + ex.Message);
                return false;
            }
        }

        //Används inte...
        //public virtual TEntity Update(TEntity entity)
        //{
        //    try
        //    {
        //        var entityToUpdate = _context.Set<TEntity>().Find(entity);

        //        if (entityToUpdate != null)
        //        {
        //            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        //            _context.SaveChanges();
        //            return entityToUpdate;
        //        }
        //        return null!;
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("BaseResUpdate: " + ex.Message);
        //        return null!;
        //    }
        //}
    }
}
