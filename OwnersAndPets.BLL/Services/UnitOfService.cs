using OwnersAndPets.BLL.Interfaces;
using OwnersAndPets.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnersAndPets.BLL.Services
{
    public class UnitOfService : IUnitOfServices
    {
        bool Disposed = false;
        private IUnitOfWork DB;

        private IOwnersService _OwnersService;
        private IPetsService _PetsService;

        public IOwnersService OwnersService
        {
            get
            {
                if (_OwnersService == null)
                    _OwnersService = new OwnersService(DB);
                return _OwnersService;
            }
        }
        public IPetsService PetsService
        {
            get
            {
                if (_PetsService == null)
                    _PetsService = new PetsService(DB);
                return _PetsService;
            }
        }

        public UnitOfService(IUnitOfWork db)
        {
            DB = db;
        }

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
