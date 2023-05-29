using AutoMapper;
using Infrastructure.BaseDomain;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Messenger.Models
{
    public class Channel : BaseEntity<Channel, ChannelCreateDto, ChannelReadDto,ChannelListDto>
    {
        public string? Description { get; set; }
        public string EndPoint { get; set; } = string.Empty;
        
        public Guid FeatureId { get; set; }

        public string? HttpRequestBody { get; set; }

        public virtual ICollection<Message>? Messages { get; set; }

        public string? AuthorizationToken { get; set; }
    }

    public class ChannelCreateDto : BaseCreateDto<Channel, ChannelCreateDto, ChannelReadDto, ChannelListDto>
    {
        public string? Description { get; set; }
        public string Endpoint { get; set; } = string.Empty;
        public Guid FeatureId { get; set; }
        public string? HttpRequestBody { get; set; }
    }
    public class ChannelReadDto: BaseReadDto<Channel, ChannelCreateDto, ChannelReadDto, ChannelListDto>
    {
        public string? Description { get; set; }
        public string Endpoint { get; set; } = string.Empty;
        public Guid FeatureId { get; set; }
        public string FeatureTitle { get; set; } = string.Empty;
        public string? HttpRequestBody { get; set; }
    }

    public class ChannelListDto : BaseListDto<Channel, ChannelCreateDto, ChannelReadDto, ChannelListDto>
    {
        public string Endpoint { get; set; } = string.Empty;
        public Guid FeatureId { get; set; }
    }

}
