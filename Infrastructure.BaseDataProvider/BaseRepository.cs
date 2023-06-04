using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.BaseDomain;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.BaseDataProvider.Repository
{
    public class BaseRepository<T, TCreateDto, TReadDto, TListDto> : IBaseRepository<T, TCreateDto, TReadDto, TListDto>
        where T : BaseEntity<T, TCreateDto, TReadDto, TListDto>
        where TCreateDto : BaseCreateDto<T, TCreateDto, TReadDto, TListDto>
        where TReadDto : BaseReadDto<T, TCreateDto, TReadDto, TListDto>
        where TListDto : BaseListDto<T, TCreateDto, TReadDto, TListDto>
    {
        protected readonly ApplicationDbContext context;
        protected readonly IMapper mapper;

        public BaseRepository(ApplicationDbContext ctx,IMapper mpr)
        {
            context = ctx;
            mapper = mpr;
        }
        public async Task<TReadDto> CreateAsync(TCreateDto entity)
        {
            context.Add(entity.GetEntity(mapper));
            await context.SaveChangesAsync();
            return entity.GetReadDto(mapper);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = context.Set<T>().FirstOrDefault(c=>c.Id == id) ?? throw new ArgumentException(nameof(id));
            entity.DeleteEntity();
            context.Entry(entity).State = EntityState.Modified;
            return await context.SaveChangesAsync()>0;
        }

        public async Task<TReadDto> GetAsync(Guid id)
        {
            var entity = await context.Set<T>().FirstOrDefaultAsync(c=>c.Id==id)??throw new ArgumentException(nameof(id));
            return entity.GetReadDto(mapper);
        }

        public IQueryable<TListDto> GetAll()
        {
            return context.Set<T>().ProjectTo<TListDto>(mapper.ConfigurationProvider).AsNoTracking();
        }

        public async Task<TReadDto> UpdateAsync(TCreateDto dto)
        {
            var tracker = context.Set<T>().Update(dto.GetEntity(mapper));
            await context.SaveChangesAsync();
            return tracker.Entity.GetReadDto(mapper);
        }

        public async Task<TReadDto> PartialUpdateAsync(Guid id, JsonPatchDocument<TCreateDto> patchDoc)
        {
            var prevDto = (await context.Set<T>().FirstOrDefaultAsync(c => c.Id == id) ?? throw new ArgumentException(nameof(id))).GetCreateDto(mapper);

            patchDoc.ApplyTo(prevDto);

            var entity = prevDto.GetEntity(mapper);

            context.Entry(entity).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return entity.GetReadDto(mapper);
        }
    }
}
