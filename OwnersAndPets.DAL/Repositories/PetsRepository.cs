using OwnersAndPets.DAL.EF;
using OwnersAndPets.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;

namespace OwnersAndPets.DAL.Repositories
{
    public class PetsRepository : IRepository<Pets>
    {
        private SQLiteContext DB;

        public PetsRepository(SQLiteContext context)
        {
            this.DB = context;
        }
        public void Create(Pets item)
        {
            if (item != null)
                DB.Pets.Add(item);
        }

        public void Delete(long id)
        {
            Pets item = DB.Pets.Find(id);

            if (item != null)
                DB.Entry(item).State = EntityState.Deleted;
            //DB.Pets.Remove(item);
        }

        public IQueryable<Pets> Find(Expression<Func<Pets, bool>> predicate)
        {
            return DB.Pets.Where(predicate);
        }

        public Pets Get(long id)
        {
            return DB.Pets.Find(id);
        }

        public IQueryable<Pets> GetAll()
        {
            return DB.Pets;
        }

        public void Update(Pets item)
        {
            if (item != null)
                DB.Entry(item).State = EntityState.Modified;
        }
    }
}
