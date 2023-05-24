using AutoMapper;
using Infrastructure.BaseDomain;
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

        public string Value { get; set; } = string.Empty;
    }

    public class ContactFeatureDto : BaseDto<ContactFeature, ContactFeatureDto, ContactFeatureReadDto>
    {
        public int ContactId { get; set; }
        public int FeatureId { get; set; }
        public string Value { get; set; } = string.Empty;


    }
    public class ContactFeatureReadDto: BaseReadDto<ContactFeature, ContactFeatureDto, ContactFeatureReadDto>
    {
        public int ContactId { get; set; }
        public string ContactTitle { get; set; } = string.Empty;
        public int FeatureId { get; set; }
        public string FeatureTitle { get; set; }= string.Empty;
        public string Value { get; set; } = string.Empty;
    }

}
