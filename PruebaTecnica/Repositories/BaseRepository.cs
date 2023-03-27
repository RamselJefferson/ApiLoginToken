using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;
using System.Linq.Expressions;

namespace PruebaTecnica.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public DbContext _context;
        public DbSet<T> dbSet;

        protected BaseRepository(DbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
            
        }
  
        public void Delete(T entity)
        {
            dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

        public T GetFirst(Expression<Func<T, bool>> filter)
        {
            T data = dbSet.AsNoTracking().FirstOrDefault(filter);
            return data;
        }

        public void Add(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }


        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> data = dbSet.AsNoTracking().ToList();
            return data;
        }


    }
}
