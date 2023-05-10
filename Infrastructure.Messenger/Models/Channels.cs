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

        public Channel GetEntity(ChannelDto Dto)
        {
            Name = Dto.Name;
            Description = Dto.Description;
            EndPoint = Dto.Endpoint;
            RequestType = Dto.RequestType;
            FeatureId = Dto.FeatureId;
            Body = Dto.Body;
            return this;
        }

        public ChannelDto GetDto()
        {
            return new ChannelDto(Name, Description,EndPoint,RequestType,FeatureId, Body);
        }

        public ChannelReadDto GetReadDto()
        {
            return new ChannelReadDto(Id, Name, Description, EndPoint, RequestType, FeatureId, Body);
        }
    }

    public record ChannelDto(string Name, string? Description, string Endpoint,ChannelRequestType RequestType, int FeatureId, string? Body):BaseDto();
    public record ChannelReadDto(int Id, string Name, string? Description, string Endpoint,ChannelRequestType RequestType, int FeatureId, string? Body):BaseReadDto(Id);

    public enum ChannelRequestType
    {
        GET,
        POST,
    }
}
