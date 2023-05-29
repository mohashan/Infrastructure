using AutoMapper.QueryableExtensions;
using Infrastructure.BaseDomain;
using Infrastructure.BaseTools;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.BaseControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController<TEntity, TCreateDto, TReadDto,TListDto> : ControllerBase
    where TEntity : BaseEntity<TEntity, TCreateDto, TReadDto, TListDto>
    where TCreateDto : BaseCreateDto<TEntity, TCreateDto, TReadDto, TListDto>
    where TReadDto : BaseReadDto<TEntity, TCreateDto, TReadDto, TListDto>
    where TListDto : BaseListDto<TEntity, TCreateDto, TReadDto, TListDto>
    {
        protected readonly ApplicationDbContext ctx;
        private readonly AutoMapper.IConfigurationProvider configurationProvider;
        protected readonly AutoMapper.Mapper mapper;

        public GenericController(ApplicationDbContext context, AutoMapper.IConfigurationProvider configurationProvider)
        {
            this.ctx = context;
            this.configurationProvider = configurationProvider;
            mapper = new AutoMapper.Mapper(configurationProvider);
        }
        [HttpGet]
        public async Task<ActionResult> Index(int count = 10, int pageNumber = 1)
        {
            IQueryable? result = ctx.Set<TEntity>().Skip(count * (pageNumber - 1)).Take(count).AsNoTracking().ProjectTo(typeof(TListDto), configurationProvider);
            return Ok(new StandardResponse<IQueryable>(true, null, result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Details(Guid id)
        {
            TEntity? item = (await ctx.Set<TEntity>().FindAsync(id)) ?? throw new ArgumentException($"There is no entry with this id(id:{id})");

            return Ok(new StandardResponse<TReadDto>(true, null, item.GetReadDto(mapper)));
        }

        [HttpPost]
        public virtual async Task<ActionResult> Create([FromBody] TCreateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            TEntity? entity = dto.GetEntity(mapper);

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
                new { id = entity.Id }, new StandardResponse<TReadDto>(true, null, entity.GetReadDto(mapper)));
        }

        [HttpPatch("{id}")]
        public ActionResult<TEntity> PartiallyUpdate(Guid id, [FromBody] JsonPatchDocument<TCreateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                throw new ArgumentNullException(nameof(patchDoc));
            }

            TCreateDto? existingDto = (ctx.Set<TEntity>().Find(id) ?? throw new ArgumentException($"There is no entry with id : {id}")).GetCreateDto(mapper);


            patchDoc.ApplyTo(existingDto);

            TEntity? entity = existingDto.GetEntity(mapper);

            TryValidateModel(entity);

            ctx.Set<TEntity>().Update(entity);


            ctx.SaveChanges();


            return Ok(new StandardResponse<TEntity>(true, null, entity));
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(Guid id)
        {
            TEntity? entity = ctx.Set<TEntity>().Find(id) ?? throw new ArgumentException($"There is no entry with id : {id}");


            entity.IsDeleted = true;
            entity.DeleteDate = DateTime.Now;
            ctx.Entry<TEntity>(entity).State = EntityState.Modified;

            ctx.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TEntity>> Update(Guid id, [FromBody] TCreateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (!await ctx.Set<TEntity>().AnyAsync(c => c.Id == id))
                throw new ArgumentException($"There is no entry with id : {id}");

            TEntity existingItem = dto.GetEntity(mapper);
            existingItem.Id = id;
            ctx.Entry<TEntity>(existingItem).State = EntityState.Modified;
            ctx.SaveChanges();

            return Ok(new StandardResponse<TCreateDto>(true, null, existingItem.GetCreateDto(mapper)));
        }
    }
}