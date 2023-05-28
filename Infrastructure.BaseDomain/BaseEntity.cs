using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Infrastructure.BaseDomain
{
    public class BaseEntity<T, TCreateDto, TReadDto,TListDto>
    where T : BaseEntity<T, TCreateDto, TReadDto,TListDto>
    where TCreateDto : BaseCreateDto<T, TCreateDto ,TReadDto, TListDto>
    where TReadDto : BaseReadDto<T, TCreateDto, TReadDto,TListDto>
    where TListDto : BaseListDto<T,TCreateDto,TReadDto, TListDto>
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(50,ErrorMessage = "Maximum length of name can be 50")]
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

        public virtual TListDto GetListDto(IMapper mapper)
        {
            return mapper.Map<TListDto>(this);
        }

        public virtual TCreateDto GetCreateDto(IMapper mapper)
        {
            return mapper.Map<TCreateDto>(this);
        }
    }



    public class BaseCreateDto<T, TCreateDto, TReadDto, TListDto>
    where T : BaseEntity<T, TCreateDto, TReadDto, TListDto>
    where TCreateDto : BaseCreateDto<T, TCreateDto, TReadDto, TListDto>
    where TReadDto : BaseReadDto<T, TCreateDto, TReadDto, TListDto>
    where TListDto : BaseListDto<T, TCreateDto, TReadDto, TListDto>
    {
        [MaxLength(50, ErrorMessage = "Maximum length of name can be 50")]
        public string Name { get; set; } = string.Empty;

        public virtual T GetEntity(IMapper mapper)
        {
            return mapper.Map<T>(this);
        }

        public virtual TReadDto GetReadDto(IMapper mapper)
        {
            return mapper.Map<TReadDto>(this);
        }
    }
    public class BaseReadDto<T, TCreateDto, TReadDto,TListDto>
    where T : BaseEntity<T, TCreateDto, TReadDto, TListDto>
    where TCreateDto : BaseCreateDto<T, TCreateDto, TReadDto, TListDto>
    where TReadDto : BaseReadDto<T, TCreateDto, TReadDto, TListDto>
    where TListDto : BaseListDto<T, TCreateDto, TReadDto, TListDto>
    {
        public string Name { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public T GetEntity(IMapper mapper)
        {
            return mapper.Map<T>(this);
        }
    }

    public class BaseListDto<T, TCreateDto, TReadDto, TListDto>
        where T : BaseEntity<T, TCreateDto, TReadDto, TListDto>
    where TCreateDto : BaseCreateDto<T, TCreateDto, TReadDto, TListDto>
    where TReadDto : BaseReadDto<T, TCreateDto, TReadDto, TListDto>
    where TListDto : BaseListDto<T, TCreateDto, TReadDto, TListDto>
    {
        public string Name { get; set; } = string.Empty;
        public Guid Id { get; set; }
    }


}