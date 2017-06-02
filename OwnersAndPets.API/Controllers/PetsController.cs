using AutoMapper;
using OwnersAndPets.API.Models;
using OwnersAndPets.BLL.DTO;
using OwnersAndPets.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OwnersAndPets.API.Controllers
{
    public class PetsController : ApiController
    {
        IUnitOfServices DB;

        public PetsController(IUnitOfServices db)
        {
            DB = db;
        }

        public IHttpActionResult Get()
        {
            return Ok(DB.OwnersService.Get());
        }
        // GET: api/Pets/5
        public IHttpActionResult Get(int id)
        {
            Mapper.Initialize(cfg => { cfg.CreateMap<DTO_Pet, Pet>(); });
            Pet item = Mapper.Map<DTO_Pet, Pet>(DB.PetsService.Get(id));

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        public IHttpActionResult Get(int? page, int itemsCount)
        {
            page = page ?? 1;
            int total = 0;
            Mapper.Initialize(cfg => { cfg.CreateMap<DTO_Pet, Pet>(); });
            IEnumerable<Pet> list = Mapper.Map<IEnumerable<DTO_Pet>, List<Pet>>(DB.PetsService.Get(itemsCount, (Int32)page, out total));

            IndexViewModel<Pet> res = new IndexViewModel<Pet>() { Items = list, PageInfo = new PageInfo { PageNumber = (Int32)page, PageSize = itemsCount, TotalItems = total } };

            return Ok(res);
        }

        public IHttpActionResult Get(int? page, int itemsCount, int ownerId)
        {
            page = page ?? 1;
            int total = 0;
            var items = DB.PetsService.Get(ownerId, itemsCount, (Int32)page, out total);
            Mapper.Initialize(cfg => { cfg.CreateMap<DTO_Pet, Pet>(); });
            IEnumerable<Pet> list = Mapper.Map<IEnumerable<DTO_Pet>, List<Pet>>(items);

            IndexViewModel<Pet> res = new IndexViewModel<Pet>() { Items = list, PageInfo = new PageInfo { PageNumber = (Int32)page, PageSize = itemsCount, TotalItems = total } };

            return Ok(res);
        }

        // POST: api/Pets
        public IHttpActionResult Post([FromBody]Pet item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Initialize(cfg => { cfg.CreateMap<Pet, DTO_Pet>(); });
            DTO_Pet res = Mapper.Map<Pet, DTO_Pet>(item);

            DB.PetsService.Create(res);

            return Ok();
        }

        // PUT: api/Pets/5
        public IHttpActionResult Put(int id, Pet model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DTO_Pet item = DB.PetsService.Get(id);

            if (item == null)
                return NotFound();

            Mapper.Initialize(cfg => { cfg.CreateMap<Pet, DTO_Pet>(); });
            DTO_Pet res = Mapper.Map<Pet, DTO_Pet>(model);
            res.Id = id;

            DB.PetsService.Update(res);

            return Ok();

        }

        // DELETE: api/Pets/5
        public IHttpActionResult Delete(int id)
        {
            if (DB.PetsService.Get(id) == null)
                return NotFound();

            DB.PetsService.Delete(id);

            return Ok();
        }
    }
}
