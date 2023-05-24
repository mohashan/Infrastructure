using Infrastructure.BaseDomain;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Messenger.Models
{
    public class Template : BaseEntity<Template, TemplateDto, TemplateReadDto>
    {
        [Required]
        public string Body { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

    }

    public class TemplateDto : BaseDto<Template, TemplateDto, TemplateReadDto>
    {
        public string Body { get; set; }

    }
    public class TemplateReadDto: BaseReadDto<Template, TemplateDto, TemplateReadDto>
    {
        public string Body { get; set; }
        public int MessagesCount { get; set; }
    }
}
