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
    public class OwnersRepository : IRepository<Owners>
    {
        private SQLiteContext DB;

        public OwnersRepository(SQLiteContext context)
        {
            this.DB = context;
        }

        public void Create(Owners item)
        {
            if (item != null)
                DB.Owners.Add(item);
        }

        public void Delete(long id)
        {
            Owners item = DB.Owners.Find(id);

            if (item != null)
                DB.Entry(item).State = EntityState.Deleted;

            //DB.Owners.Remove(item);
        }

        public IQueryable<Owners> Find(Expression<Func<Owners, bool>> predicate)
        {
            return DB.Owners.Where(predicate);
        }

        public Owners Get(long id)
        {
            return DB.Owners.Find(id);
        }

        public IQueryable<Owners> GetAll()
        {
            return DB.Owners.Include(x=>x.Pets);
        }

        public void Update(Owners item)
        {
            if (item != null)
                DB.Entry(item).State = EntityState.Modified;
        }
    }
}
