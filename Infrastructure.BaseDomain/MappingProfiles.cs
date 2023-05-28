using AutoMapper;
using System.Reflection;

namespace Infrastructure.BaseDomain
{
    public class MappingProfiles<T,TCreateDto,TReadDto,TListDto> : Profile
        where T : BaseEntity<T, TCreateDto, TReadDto, TListDto>
    where TCreateDto : BaseCreateDto<T, TCreateDto, TReadDto, TListDto>
    where TReadDto : BaseReadDto<T, TCreateDto, TReadDto, TListDto>
    where TListDto : BaseListDto<T, TCreateDto, TReadDto, TListDto>
    {
        public MappingProfiles()
        {
            Assembly.GetExecutingAssembly().GetTypes().Where(c=>c.IsEntity<T,TCreateDto,TReadDto,TListDto>()).ToList().ForEach(type =>
            {

                    var baseType = type.BaseType;
                    var entityType = baseType?.GetGenericArguments()[0];
                    var createDtoType = baseType?.GetGenericArguments()[1];
                    var readDtoType = baseType?.GetGenericArguments()[2];
                    var listDtoType = baseType?.GetGenericArguments()[2];

                    CreateMap(entityType, readDtoType);
                    CreateMap(entityType, listDtoType);
                    CreateMap(readDtoType, createDtoType).ReverseMap();
                    CreateMap(createDtoType, entityType).ReverseMap();
            });
        }
    }
}
