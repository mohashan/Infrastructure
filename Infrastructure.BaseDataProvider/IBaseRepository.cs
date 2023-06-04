using Infrastructure.BaseDomain;
using Microsoft.AspNetCore.JsonPatch;

namespace Infrastructure.BaseDataProvider.Repository
{
    public interface IBaseRepository<T,TCreateDto,TReadDto,TListDto>
        where T : BaseEntity<T,TCreateDto,TReadDto, TListDto>
        where TCreateDto : BaseCreateDto<T,TCreateDto,TReadDto, TListDto>
        where TReadDto : BaseReadDto<T,TCreateDto,TReadDto, TListDto>
        where TListDto : BaseListDto<T,TCreateDto,TReadDto, TListDto>
    {
        Task<TReadDto> GetAsync(Guid id);
        IQueryable<TListDto> GetAll();

        Task<TReadDto> CreateAsync(TCreateDto entity);

        Task<bool> DeleteAsync(Guid id);

        Task<TReadDto> UpdateAsync(TCreateDto dto);

        Task<TReadDto> PartialUpdateAsync(Guid id,JsonPatchDocument<TCreateDto> patchDoc);
    }
}