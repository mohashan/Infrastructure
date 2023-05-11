using AutoMapper.QueryableExtensions;
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
            return Ok(ctx.Set<TEntity>().Skip(count * (pageNumber - 1)).Take(count).AsNoTracking().ProjectTo(typeof(TReadDto),configurationProvider));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Details(int id)
        {
            var item = await ctx.Set<TEntity>().FindAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item.GetReadDto(mapper));
        }
        
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
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
                throw new Exception("Create new Entity failed");
            }
            


            return CreatedAtAction(nameof(Details),
                new {  id = entity.Id },entity.GetReadDto(mapper));
        }

        [HttpPatch("{id:int}")]
        public ActionResult<TEntity> PartiallyUpdate(int id, [FromBody] JsonPatchDocument<TDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var existingDto= ctx.Set<TEntity>().Find(id)?.GetDto(mapper);

            if (existingDto == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(existingDto);

            var entity = new BaseEntity<TEntity,TDto,TReadDto>().GetEntity(existingDto, mapper);

            TryValidateModel(entity);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ctx.Set<TEntity>().Update(entity);

            try
            {

                ctx.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Update a entity failed on save");
            }


            return Ok(entity);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Remove(int id)
        {
            var entity = ctx.Set<TEntity>().Find(id);

            if (entity == null)
            {
                return NotFound();
            }

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
