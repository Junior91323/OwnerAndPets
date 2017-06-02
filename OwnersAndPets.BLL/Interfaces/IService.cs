using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnersAndPets.BLL.Interfaces
{
   public interface IService<T>
    {
        void Create(T item);
        void Update(T item);
        T Get(int id);
        void Delete(int id);
        IEnumerable<T> Get();
        IEnumerable<T> Get(int pageSize, int page, out int total);
        void Dispose();
    }
}
