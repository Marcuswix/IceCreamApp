using Microsoft.EntityFrameworkCore.Update.Internal;
using Share.Contexts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Share.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;

        protected BaseRepository(DataContext context) 
        {
            _context = context;
        }

        //Virtual gör så att koden går att modifiera
        public virtual TEntity Create(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex) { Debug.WriteLine("BaseResCreate" + ex.Message);
                return null!;
            }
        }

        public virtual IEnumerable<TEntity> GetAll() 
        {
            try
            {
                var result = _context.Set<TEntity>().ToList();
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("BaseResGetAll" + ex.Message);
                return null!;
            }
        }

        public virtual TEntity GetOne(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var result = _context.Set<TEntity>().FirstOrDefault(predicate);

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

        public virtual TEntity Update(TEntity entity)
        {
            try
            {
                var entityToUpdate = _context.Set<TEntity>().Find(entity);

                if (entityToUpdate != null)
                {
                    _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                    _context.SaveChanges();
                    return entityToUpdate;
                }
                return null!;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("BaseResUpdate: " + ex.Message);
                return null!;
            }
        }

        public virtual bool Delete(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var entityToDelete = _context.Set<TEntity>().Find(predicate);

                if(entityToDelete != null)
                {
                    _context.Remove(entityToDelete);
                    _context.SaveChanges();
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("BaseResDelete" + ex.Message);
                return false;
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
    }
}
