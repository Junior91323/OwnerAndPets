using OwnersAndPets.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnersAndPets.BLL.Interfaces
{
  public  interface IOwnersService:IService<DTO_Owner>
    {
        IEnumerable<DTO_Owner> GetWithoutAttachments(int pageSize, int page, out int total);
    }
}
