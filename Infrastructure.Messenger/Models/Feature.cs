using AutoMapper;
using Infrastructure.BaseDomain;
using System;
using System.Text.Json.Serialization;

namespace Infrastructure.Messenger.Models
{
    public class Feature : BaseEntity<Feature, FeatureDto, FeatureReadDto>
    {
        public void FillDataType(Type type)
        {
            DataType = typeof(Type)?.FullName ?? typeof(string).FullName;
        }
        public string DataType { get; set; } = typeof(string).FullName;

        public string? Description { get; set; }
        [JsonIgnore]
        public virtual ICollection<ContactFeature> ContactFeatures { get; set; }
        [JsonIgnore]
        public virtual ICollection<Channel> Channels { get; set; }


    }

    public class FeatureDto: BaseDto<Feature, FeatureDto, FeatureReadDto>
    {
        public string DataType { get; set; } 
        public string? Description { get; set; }


    }
    public class FeatureReadDto : BaseReadDto<Feature, FeatureDto, FeatureReadDto>
    {
        public string DataType { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

}
