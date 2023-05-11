using AutoMapper;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Messenger.Models
{
    public class Channel:BaseEntity<Channel,ChannelDto,ChannelReadDto>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string EndPoint { get; set; }
        public ChannelRequestType RequestType { get; set; }
        [ForeignKey(nameof(Feature))]
        public int FeatureId { get; set; }
        [JsonIgnore]
        public Feature? Feature { get; set; }
        public string? Body { get; set; }

    }

    public record ChannelDto(string Name, string? Description, string Endpoint,ChannelRequestType RequestType, int FeatureId, string? Body):BaseDto();
    public record ChannelReadDto(int Id, string Name, string? Description, string Endpoint,ChannelRequestType RequestType, int FeatureId, string? Body):BaseReadDto(Id);

    public enum ChannelRequestType
    {
        GET,
        POST,
    }
}
