using OwnersAndPets.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnersAndPets.BLL.Interfaces
{
    public interface IPetsService : IService<DTO_Pet>
    {
        IEnumerable<DTO_Pet> Get(int ownerId, int pageSize, int page, out int total);
    }
}
