using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Infrastructure.Messenger.Models
{
    public class BaseEntity<T,TDto,TReadDto>
        where T : BaseEntity<T,TDto,TReadDto>
        where TDto : BaseDto
        where TReadDto : BaseReadDto
    {

        
        [Key]
        public int Id { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime? DeleteDate { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;

        public virtual T GetEntity(TDto dto,IMapper mapper)
        {
            return mapper.Map<T>(dto);
        }

        public virtual TReadDto GetReadDto(IMapper mapper)
        {
            return mapper.Map<TReadDto>(this);
        }

        public virtual TDto GetDto(IMapper mapper)
        {
            return mapper.Map<TDto>(this);
        }
    }

    public record BaseDto();
    public record BaseReadDto(int Id);
}