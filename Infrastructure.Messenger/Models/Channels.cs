using AutoMapper;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Messenger.Models
{
    public class Channel:BaseEntity<Channel,ChannelDto,ChannelReadDto>
    {
        public string? Description { get; set; }
        public string EndPoint { get; set; } = string.Empty;
        [ForeignKey(nameof(Feature))]
        public int FeatureId { get; set; }
        [JsonIgnore]
        public Feature? Feature { get; set; }

        public string? HttpRequestBody { get; set; }

        public virtual ICollection<Message>? Messages { get; set; }

        public string? AuthorizationToken { get; set; }
    }

    public record ChannelDto(string? title, string? Description, string Endpoint, int FeatureId,string HttpRequestBody) :BaseDto(title);
    public record ChannelReadDto(int Id, string? title, string? Description, string Endpoint, int FeatureId, string HttpRequestBody,DateTime InsertDate) :BaseReadDto(Id, title, InsertDate);

}
