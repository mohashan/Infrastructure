﻿using Infrastructure.BaseDomain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Infrastructure.BaseUserManager.Models
{
    public class UserFeature : BaseEntity<UserFeature, UserFeatureCreateDto, UserFeatureReadDto,UserFeatureListDto>
    {

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

        [ForeignKey(nameof(Feature))]
        public Guid FeatureId { get; set; }
        [JsonIgnore]
        public Feature Feature { get; set; }

        public string Value { get; set; } = string.Empty;
    }

    class UserFeatureEntityConfiguration : IEntityTypeConfiguration<UserFeature>
    {
        public void Configure(EntityTypeBuilder<UserFeature> builder)
        {
        }
    }

    public class UserFeatureCreateDto : BaseCreateDto<UserFeature, UserFeatureCreateDto, UserFeatureReadDto,UserFeatureListDto>
    {
        public Guid UserId { get; set; }
        public Guid FeatureId { get; set; }
        public string Value { get; set; } = string.Empty;


    }
    public class UserFeatureReadDto : BaseReadDto<UserFeature, UserFeatureCreateDto, UserFeatureReadDto, UserFeatureListDto>
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public Guid FeatureId { get; set; }
        public string FeatureName { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
    public class UserFeatureListDto : BaseListDto<UserFeature, UserFeatureCreateDto, UserFeatureReadDto, UserFeatureListDto>
    {
        public Guid UserId { get; set; }
        public Guid FeatureId { get; set; }
        public string Value { get; set; } = string.Empty;


    }
}
