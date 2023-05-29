using Infrastructure.BaseDomain;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Messenger.Models
{
    public class Template : BaseEntity<Template, TemplateCreateDto, TemplateReadDto, TemplateListDto>
    {
        [Required]
        public string Body { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

    }

    public class TemplateCreateDto : BaseCreateDto<Template, TemplateCreateDto, TemplateReadDto,TemplateListDto>
    {
        [Required]
        public string Body { get; set; }

    }
    public class TemplateReadDto: BaseReadDto<Template, TemplateCreateDto, TemplateReadDto, TemplateListDto>
    {
        public string Body { get; set; }
        public int MessagesCount { get; set; }
    }

    public class TemplateListDto : BaseListDto<Template, TemplateCreateDto, TemplateReadDto, TemplateListDto>
    {
        public int MessagesCount { get; set; }
    }
}
