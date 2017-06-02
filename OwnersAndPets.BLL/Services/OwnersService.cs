using AutoMapper;
using OwnersAndPets.BLL.DTO;
using OwnersAndPets.BLL.Interfaces;
using OwnersAndPets.DAL.EF;
using OwnersAndPets.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnersAndPets.BLL.Services
{
    public class OwnersService : IOwnersService
    {

        private IUnitOfWork DB;
        public OwnersService(IUnitOfWork db)
        {
            DB = db;
        }

        public void Create(DTO_Owner item)
        {
            try
            {
                if (item == null)
                    throw new NullReferenceException("item is null");

                Owners owner = DB.Owners.Get(item.Id);

                if (owner != null)
                    throw new Exception("Owner already exist");

                Owners res = new Owners
                {
                    Name = item.Name
                };
                DB.Owners.Create(res);
                DB.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var list = DB.Pets.Find(x => x.Owners_Id == id);
                foreach (var item in list)
                {
                    DB.Pets.Delete(item.Id);
                }
                DB.Owners.Delete(id);
                DB.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public void Dispose()
        {
            DB.Dispose();
        }

        public DTO_Owner Get(int id)
        {

            var item = DB.Owners.Get(id);

            if (item != null)
            {
                Mapper.Initialize(cfg => { cfg.CreateMap<Owners, DTO_Owner>(); cfg.CreateMap<Pets, DTO_Pet>().ForMember("OwnerId", opt => opt.MapFrom(c => c.Owners_Id)); });
                return Mapper.Map<Owners, DTO_Owner>(item);
            }

            return null;

        }

        public IEnumerable<DTO_Owner> Get()
        {
            try
            {
                Mapper.Initialize(cfg => { cfg.CreateMap<Owners, DTO_Owner>(); cfg.CreateMap<Pets, DTO_Pet>().ForMember("OwnerId", opt => opt.MapFrom(c => c.Owners_Id)); });
                return Mapper.Map<IEnumerable<Owners>, List<DTO_Owner>>(DB.Owners.GetAll().OrderByDescending(x => x.Id).ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public IEnumerable<DTO_Owner> Get(int pageSize, int page, out int total)
        {
            try
            {
                var res = DB.Owners.GetAll().OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                total = DB.Owners.GetAll().Count();
                Mapper.Initialize(cfg => { cfg.CreateMap<Owners, DTO_Owner>(); cfg.CreateMap<Pets, DTO_Pet>().ForMember("OwnerId", opt => opt.MapFrom(c => c.Owners_Id)); });
                return Mapper.Map<IEnumerable<Owners>, List<DTO_Owner>>(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public IEnumerable<DTO_Owner> GetWithoutAttachments(int pageSize, int page, out int total)
        {
            try
            {
                IEnumerable<DTO_Owner> res = DB.Owners.GetAll().Select(x => new DTO_Owner { Id = x.Id, Name = x.Name, PetsCount = x.Pets.Count() }).OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                total = DB.Owners.GetAll().Count();


                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public void Update(DTO_Owner item)
        {
            try
            {
                if (item == null)
                    throw new NullReferenceException("item is null");

                Owners owner = DB.Owners.Get(item.Id);

                if (owner == null)
                    throw new Exception(String.Format("Item with id: {0} is not found!", item.Id));

                owner.Name = item.Name;

                DB.Owners.Update(owner);
                DB.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
