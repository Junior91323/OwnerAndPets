using OwnersAndPets.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OwnersAndPets.DAL.EF;

namespace OwnersAndPets.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private SQLiteContext DB;
        private OwnersRepository OwnersRepository;
        private PetsRepository PetsRepository;

        public IRepository<Owners> Owners
        {
            get
            {
                if (OwnersRepository == null)
                    OwnersRepository = new OwnersRepository(DB);
                return OwnersRepository;
            }
        }
        public IRepository<Pets> Pets
        {
            get
            {
                if (PetsRepository == null)
                    PetsRepository = new PetsRepository(DB);
                return PetsRepository;
            }
        }

        public UnitOfWork()
        {
            DB = new SQLiteContext();
        }

        public void Save()
        {
            try
            {
                DB.SaveChanges();
            }
            catch (Exception ex)
            {
                //Log ....
                throw new Exception("Saving Error", ex);
            }
        }

        private bool Disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                if (disposing)
                {
                    DB.Dispose();
                }
                this.Disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
