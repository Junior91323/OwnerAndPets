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
    public class OwnersController : ApiController
    {
        IUnitOfServices DB;

        public OwnersController(IUnitOfServices db)
        {
            DB = db;
        }

        // GET: api/Pets/5
        public IHttpActionResult Get(int id)
        {
            var item = DB.OwnersService.Get(id);
            Mapper.Initialize(cfg => { cfg.CreateMap<DTO_Owner, Owner>(); });
            Owner res = Mapper.Map<DTO_Owner, Owner>(item);

            if (res == null)
                return NotFound();

            return Ok(res);
        }

        public IHttpActionResult Get()
        {
            return Ok(DB.OwnersService.Get());
        }

        public IHttpActionResult Get(int? page, int itemsCount)
        {
            page = page ?? 1;
            int total = 0;
            var items = DB.OwnersService.GetWithoutAttachments(itemsCount, (Int32)page, out total);
            Mapper.Initialize(cfg => { cfg.CreateMap<DTO_Owner, Owner>(); });
            IEnumerable<Owner> list = Mapper.Map<IEnumerable<DTO_Owner>, List<Owner>>(items);

            IndexViewModel<Owner> res = new IndexViewModel<Owner>() { Items = list, PageInfo = new PageInfo { PageNumber = (Int32)page, PageSize = itemsCount, TotalItems = total } };

            return Ok(res);
        }


        // POST: api/Pets
        public IHttpActionResult Post([FromBody]Owner item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Initialize(cfg => { cfg.CreateMap<Owner, DTO_Owner>(); });
            DTO_Owner res = Mapper.Map<Owner, DTO_Owner>(item);

            DB.OwnersService.Create(res);

            return Ok();
        }

        // PUT: api/Pets/5
        public IHttpActionResult Put(int id, Owner model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            DTO_Owner item = DB.OwnersService.Get(id);

            if (item == null)
                return NotFound();

            Mapper.Initialize(cfg => { cfg.CreateMap<Owner, DTO_Owner>(); });
            DTO_Owner res = Mapper.Map<Owner, DTO_Owner>(model);


            DB.OwnersService.Update(res);

            return Ok();

        }

        // DELETE: api/Pets/5
        public IHttpActionResult Delete(int id)
        {
            if (DB.OwnersService.Get(id) == null)
                return NotFound();

            DB.OwnersService.Delete(id);

            return Ok();
        }
    }
}
