using Infrastructure.BaseDomain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Infrastructure.BaseUserManager.Models
{
    public class UserFeature : BaseEntity<UserFeature, UserFeatureDto, UserFeatureReadDto>
    {

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }

        [ForeignKey(nameof(Feature))]
        public int FeatureId { get; set; }
        [JsonIgnore]
        public virtual Feature Feature { get; set; }

        public string Value { get; set; } = string.Empty;
    }

    public class UserFeatureDto : BaseDto<UserFeature, UserFeatureDto, UserFeatureReadDto>
    {
        public int UserId { get; set; }
        public int FeatureId { get; set; }
        public string Value { get; set; } = string.Empty;


    }
    public class UserFeatureReadDto : BaseReadDto<UserFeature, UserFeatureDto, UserFeatureReadDto>
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int FeatureId { get; set; }
        public string FeatureName { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
