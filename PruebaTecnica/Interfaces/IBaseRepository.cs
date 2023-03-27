using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PruebaTecnica.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
       


        void Delete(T entity);

        void Update(T entity);

        T GetFirst(Expression<Func<T, bool>> filter);

        void Add(T entity);


        IEnumerable<T> GetAll();







    }
}
