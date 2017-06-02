using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnersAndPets.BLL.DTO
{
  public  class DTO_Pet
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long OwnerId { get; set; }

        //public DTO_Owner Owner { get; set; }
    }
}
