using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnersAndPets.BLL.Interfaces
{
  public interface IUnitOfServices : IDisposable
    {
        IOwnersService OwnersService { get; }
        IPetsService PetsService { get; }
    }
}
