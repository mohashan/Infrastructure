using AutoMapper.QueryableExtensions;
using Infrastructure.BaseTools;
using Infrastructure.Messenger.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace Infrastructure.Messenger.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController<TEntity,TDto,TReadDto> : ControllerBase
        where TEntity : BaseEntity<TEntity,TDto,TReadDto>
        where TDto : BaseDto 
        where TReadDto : BaseReadDto
    {
        protected readonly MessengerDbContext ctx;
        private readonly AutoMapper.IConfigurationProvider configurationProvider;
        protected readonly AutoMapper.Mapper mapper;

        public GenericController(MessengerDbContext context,AutoMapper.IConfigurationProvider configurationProvider)
        {
            this.ctx = context;
            this.configurationProvider = configurationProvider;
            mapper = new AutoMapper.Mapper(configurationProvider);
        }
        [HttpGet]
        public async Task<ActionResult> Index(int count = 10, int pageNumber = 1)
        {
            var result = ctx.Set<TEntity>().Skip(count * (pageNumber - 1)).Take(count).AsNoTracking().ProjectTo(typeof(TReadDto),configurationProvider);
            return Ok(new StandardResponse<IQueryable>(true, null, result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Details(int id)
        {
            var item = (await ctx.Set<TEntity>().FindAsync(id))??throw new ArgumentException($"There is no entry with this id(id:{id})");

            return Ok(new StandardResponse<TReadDto>(true,null, item.GetReadDto(mapper)));
        }
        
        [HttpPost]
        public virtual async Task<ActionResult> Create([FromBody] TDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var baseEnt = new Models.BaseEntity<TEntity,TDto,TReadDto>();
            var entity = baseEnt.GetEntity(dto,mapper);

            try
            {
                ctx.Set<TEntity>().Add(entity);
                await ctx.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException("Create new Entity failed");
            }
            


            return CreatedAtAction(nameof(Details),
                new {  id = entity.Id },new StandardResponse<TReadDto>(true,null, entity.GetReadDto(mapper)));
        }

        [HttpPatch("{id:int}")]
        public ActionResult<TEntity> PartiallyUpdate(int id, [FromBody] JsonPatchDocument<TDto> patchDoc)
        {
            if (patchDoc == null)
            {
                throw new ArgumentNullException(nameof(patchDoc));
            }

            var existingDto= (ctx.Set<TEntity>().Find(id)??throw new ArgumentException($"There is no entry with id : {id}")).GetDto(mapper);


            patchDoc.ApplyTo(existingDto);

            var entity = new BaseEntity<TEntity,TDto,TReadDto>().GetEntity(existingDto, mapper);

            TryValidateModel(entity);

            ctx.Set<TEntity>().Update(entity);


                ctx.SaveChanges();


            return Ok(new StandardResponse<TEntity>(true,null, entity));
        }

        [HttpDelete("{id:int}")]
        public ActionResult Remove(int id)
        {
            var entity = ctx.Set<TEntity>().Find(id)??throw new ArgumentException("$\"There is no entry with id : {id}");


            entity.IsDeleted = true;
            entity.DeleteDate = DateTime.Now;
            ctx.Entry<TEntity>(entity).State = EntityState.Modified;

            try
            {

                ctx.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Update an entity failed on save");
            }


            return NoContent();
        }

        [HttpPut("{id:int}")]
        public ActionResult<TEntity> Update(int id, [FromBody] TDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var existingItem = ctx.Set<TEntity>().Find(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.GetEntity(dto, mapper);

            ctx.Entry<TEntity>(existingItem).State = EntityState.Modified;
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Update a entity failed on save");
            }


            return Ok(existingItem.GetDto(mapper));
        }
    }
}
