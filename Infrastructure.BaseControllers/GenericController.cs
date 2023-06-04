using AutoMapper.QueryableExtensions;
using Infrastructure.BaseDataProvider.Repository;
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
        private readonly IBaseRepository<TEntity, TCreateDto, TReadDto, TListDto> repo;
        protected readonly AutoMapper.Mapper mapper;

        public GenericController(ApplicationDbContext context, AutoMapper.IConfigurationProvider configurationProvider,IBaseRepository<TEntity,TCreateDto,TReadDto,TListDto> repo)
        {
            this.ctx = context;
            this.configurationProvider = configurationProvider;
            this.repo = repo;
            mapper = new AutoMapper.Mapper(configurationProvider);
        }
        [HttpGet]
        public async Task<ActionResult> Index(int count = 10, int pageNumber = 1)
        {
            return Ok(await repo.GetAll().Skip(count * (pageNumber - 1)).Take(count).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Details(Guid id)
        {
            return Ok(await repo.GetAsync(id));
        }

        [HttpPost]
        public virtual async Task<ActionResult> Create([FromBody] TCreateDto dto)
        {
            TReadDto readDto = await repo.CreateAsync(dto);
            return CreatedAtAction(nameof(Details),new { id = readDto.Id}, await repo.CreateAsync(dto));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartiallyUpdate(Guid id, [FromBody] JsonPatchDocument<TCreateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                throw new ArgumentNullException(nameof(patchDoc));
            }

            return Ok(await repo.PartialUpdateAsync(id, patchDoc));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(Guid id)
        {
            return (await repo.DeleteAsync(id)?NoContent():BadRequest());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TEntity>> Update(Guid id, [FromBody] TCreateDto dto)
        {
            return Ok(await repo.UpdateAsync(dto));
        }
    }
}