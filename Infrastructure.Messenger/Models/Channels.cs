using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Messenger.Models
{
    public class Channels:BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string EndPoint { get; set; }
        public ChannelRequestType ChannelRequestType { get; set; }
        [ForeignKey(nameof(Feature))]
        public int FeatureId { get; set; }
        [JsonIgnore]
        public Feature? Feature { get; set; }
        public string? Body { get; set; }
    }

    public enum ChannelRequestType
    {
        GET,
        POST,
    }
}
