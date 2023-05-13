using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Messenger.Models
{
    public class Template : BaseEntity<Template, TemplateDto, TemplateReadDto>
    {
        [Required]
        public string Body { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

    }

    public record TemplateDto(string? title, string Body) : BaseDto(title);
    public record TemplateReadDto(int Id, string? title, string Body, DateTime InsertDate) : BaseReadDto(Id, title, InsertDate);
}
