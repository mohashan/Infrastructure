using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Infrastructure.BaseDomain
{
    public class BaseEntity<T, TDto, TReadDto>
    where T : BaseEntity<T, TDto, TReadDto>
    where TDto : BaseDto<T, TDto, TReadDto>
    where TReadDto : BaseReadDto<T, TDto, TReadDto>
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime InsertDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime? DeleteDate { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;



        public virtual TReadDto GetReadDto(IMapper mapper)
        {
            return mapper.Map<TReadDto>(this);
        }

        public virtual TDto GetDto(IMapper mapper)
        {
            return mapper.Map<TDto>(this);
        }
    }

    public class BaseDto<T, TDto, TReadDto>
    where T : BaseEntity<T, TDto, TReadDto>
    where TDto : BaseDto<T, TDto, TReadDto>
    where TReadDto : BaseReadDto<T, TDto, TReadDto>
    {
        public string Name { get; set; } = string.Empty;

        public virtual T GetEntity(IMapper mapper)
        {
            return mapper.Map<T>(this);
        }
    }
    public class BaseReadDto<T, TDto, TReadDto>
    where T : BaseEntity<T, TDto, TReadDto>
    where TDto : BaseDto<T, TDto, TReadDto>
    where TReadDto : BaseReadDto<T, TDto, TReadDto>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public virtual T GetEntity(IMapper mapper)
        {
            return mapper.Map<T>(this);
        }
    }
}