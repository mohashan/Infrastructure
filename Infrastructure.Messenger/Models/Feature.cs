using AutoMapper;
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
        public string DataType { get; set; } = typeof(string)?.FullName;

        public string? Description { get; set; }
        [JsonIgnore]
        public virtual ICollection<ContactFeature> ContactFeatures { get; set; }
        [JsonIgnore]
        public virtual ICollection<Channel> Channels { get; set; }


    }

    public record FeatureDto(string DataType, string title, string? Description) : BaseDto(title);
    public record FeatureReadDto(int Id, string DataType, string title, string? Description, DateTime InsertDate) : BaseReadDto(Id, title, InsertDate);

}
