using AutoMapper;
using System.Reflection;

namespace Infrastructure.BaseDomain
{
    public class MappingProfiles<T,TDto,TReadDto> : Profile
        where T : BaseEntity<T, TDto, TReadDto>
        where TDto : BaseDto<T, TDto, TReadDto>
        where TReadDto : BaseReadDto<T, TDto, TReadDto>
    {
        public MappingProfiles()
        {
            Assembly.GetExecutingAssembly().GetTypes().Where(c=>c.IsEntity<T,TDto,TReadDto>()).ToList().ForEach(type =>
            {

                    var baseType = type.BaseType;
                    var entityType = baseType?.GetGenericArguments()[0];
                    var dtoType = baseType?.GetGenericArguments()[1];
                    var readDtoType = baseType?.GetGenericArguments()[2];

                    CreateMap(entityType, readDtoType).ReverseMap();
                    CreateMap(readDtoType, dtoType).ReverseMap();
                    CreateMap(dtoType, entityType).ReverseMap();
            });
        }
    }
}
