using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OwnersAndPets.DAL.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T Get(long id);
        IQueryable<T> Find(Expression<Func<T, Boolean>> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(long id);
    }
}
