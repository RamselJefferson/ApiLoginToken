using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PruebaTecnica.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
       


        void Delete(T entity);

        void Update(T entity);

        T GetFirst(Expression<Func<T, bool>> filter);

        T

        void Add(T entity);

        IEnumerable<T> Where(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();







    }
}
