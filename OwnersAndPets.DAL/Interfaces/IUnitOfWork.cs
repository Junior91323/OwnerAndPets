using OwnersAndPets.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnersAndPets.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Owners> Owners { get; }
        IRepository<Pets> Pets { get; }
        void Save();
    }
}
