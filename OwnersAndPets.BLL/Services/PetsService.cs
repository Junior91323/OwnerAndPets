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
    public class PetsService : IPetsService
    {

        private IUnitOfWork DB;
        public PetsService(IUnitOfWork db)
        {
            DB = db;
        }

        public void Create(DTO_Pet item)
        {
            try
            {
                if (item == null)
                    throw new NullReferenceException("item is null");

                Pets pet = DB.Pets.Get(item.Id);

                if (pet != null)
                    throw new Exception("Pet already exist");

                Pets res = new Pets
                {
                    Name = item.Name,
                    Owners_Id = item.OwnerId
                };
                DB.Pets.Create(res);
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
                DB.Pets.Delete(id);
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

        public DTO_Pet Get(int id)
        {

            var item = DB.Pets.Get(id);

            if (item != null)
            {

                Mapper.Initialize(cfg => cfg.CreateMap<Pets, DTO_Pet>()
                    .ForMember("OwnerId", opt => opt.MapFrom(c => c.Owners_Id)));
                    //.ForMember("Owner", opt => opt.MapFrom(src => src.Owners)));

                return Mapper.Map<Pets, DTO_Pet>(item);
            }

            return null;

        }

        public IEnumerable<DTO_Pet> Get()
        {
            try
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Pets, DTO_Pet>()
                        .ForMember("OwnerId", opt => opt.MapFrom(c => c.Owners_Id));
                       // .ForMember("Owner", opt => opt.MapFrom(src => src.Owners));
                });

                return Mapper.Map<IEnumerable<Pets>, List<DTO_Pet>>(DB.Pets.GetAll().OrderByDescending(x => x.Id).ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public IEnumerable<DTO_Pet> Get(int pageSize, int page, out int total)
        {
            try
            {
                var res = DB.Pets.GetAll().OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                total = DB.Pets.GetAll().Count();
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Pets, DTO_Pet>()
                        .ForMember("OwnerId", opt => opt.MapFrom(c => c.Owners_Id));
                        //.ForMember("Owner", opt => opt.MapFrom(src => src.Owners));
                });
                return Mapper.Map<IEnumerable<Pets>, List<DTO_Pet>>(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        public IEnumerable<DTO_Pet> Get(int ownerId, int pageSize, int page, out int total)
        {
            try
            {
                var res = DB.Pets.GetAll().Where(x => x.Owners_Id == ownerId).OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                total = DB.Pets.GetAll().Where(x=>x.Owners_Id == ownerId).Count();
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Pets, DTO_Pet>()
                        .ForMember("OwnerId", opt => opt.MapFrom(c => c.Owners_Id));
                        //.ForMember("Owner", opt => opt.MapFrom(src => src.Owners));
                });
                return Mapper.Map<IEnumerable<Pets>, List<DTO_Pet>>(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        public void Update(DTO_Pet item)
        {
            try
            {
                if (item == null)
                    throw new NullReferenceException("item is null");

                Pets pet = DB.Pets.Get(item.Id);

                if (pet == null)
                    throw new Exception(String.Format("Item with id: {0} is not found!", item.Id));

                pet.Name = item.Name;
                pet.Owners_Id = item.OwnerId;

                DB.Pets.Update(pet);
                DB.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }


    }
}
