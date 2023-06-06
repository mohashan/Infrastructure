using Infrastructure.BaseDomain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Infrastructure.BaseUserManager.Models
{
    public class Feature : BaseEntity<Feature, FeatureCreateDto, FeatureReadDto,FeatureListDto>
    {
        public void FillDataType(Type type)
        {
            DataType = typeof(Type)?.FullName ?? typeof(string).FullName;
        }

        [MaxLength(20,ErrorMessage = "Maximum length must be 20")]
        public string DataType { get; set; } = typeof(string).FullName;

        [MaxLength(100,ErrorMessage = "Maximum length must be 100")]
        public string? Description { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserFeature> UserFeatures { get; set; }
    }

    class FeatureEntityConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
        }
    }

    public class FeatureCreateDto : BaseCreateDto<Feature, FeatureCreateDto, FeatureReadDto, FeatureListDto>
    {
        [MaxLength(20, ErrorMessage = "Maximum length must be 20")]
        public string DataType { get; set; }

        [MaxLength(100,ErrorMessage = "Maximum length must be 100")]
        public string? Description { get; set; }


    }
    public class FeatureReadDto : BaseReadDto<Feature, FeatureCreateDto, FeatureReadDto, FeatureListDto>
    {
        public string DataType { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class FeatureListDto : BaseListDto<Feature, FeatureCreateDto, FeatureReadDto, FeatureListDto>
    {
        public string DataType { get; set; } = string.Empty;
    }
}
