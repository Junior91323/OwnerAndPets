using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnersAndPets.BLL.DTO
{
    public class DTO_Owner
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int PetsCount { get; set; }
        public IEnumerable<DTO_Pet> Pets { get; set; }
    }
}
