using AutoMapper;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Infrastructure.Messenger.Models
{
    public class ContactFeature:BaseEntity<ContactFeature,ContactFeatureDto,ContactFeatureReadDto>
    {

        [ForeignKey(nameof(Contact))]
        public int ContactId { get; set; }
        [JsonIgnore]
        public virtual Contact Contact { get; set; }

        [ForeignKey(nameof(Feature))]
        public int FeatureId { get; set; }
        [JsonIgnore]
        public virtual Feature Feature { get; set; }

        public string Value { get; set; }
    }

    public record ContactFeatureDto(string? title,int ContactId, int FeatureId, string Value):BaseDto(title);
    public record ContactFeatureReadDto(int Id, string? title, int ContactId, int FeatureId, string Value,DateTime InsertDate) : BaseReadDto(Id,title,InsertDate);
}
