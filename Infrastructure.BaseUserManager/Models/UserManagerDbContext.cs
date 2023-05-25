using Infrastructure.BaseDomain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infrastructure.BaseUserManager.Models
{
    public class UserManagerDbContext : ApplicationDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserType>().HasQueryFilter(c => !c.IsDeleted).HasData(new UserType[]
            {
                new UserType { Id=1,Name = "People" },
                new UserType { Id = 2,Name = "Service" },
                new UserType { Id= 3,Name = "Device" },
            });

            modelBuilder.Entity<Group>().HasQueryFilter(c => !c.IsDeleted).HasData(new Group[]
            {
                new Group { Id=1,Name = "G1" },
            });

            modelBuilder.Entity<User>().HasQueryFilter(c => !c.IsDeleted).HasData(new User[]
            {
                new User { Id=1,Name = "Admin",UserTypeId = 1 },
                new User { Id = 2,Name = "SupportUser",UserTypeId = 1 },
            });

            modelBuilder.Entity<UserGroup>().HasQueryFilter(c => !c.IsDeleted).HasData(new UserGroup[]
            {
                new UserGroup { Id=1,Name = "Admin",UserId = 1,GroupId=1 },
                new UserGroup { Id = 2,Name = "SupportUser",UserId = 2,GroupId = 1 },
            });
            modelBuilder.Entity<UserGroup>().HasIndex(c => new { c.UserId, c.GroupId });

            modelBuilder.Entity<UserFeature>().HasQueryFilter(c => !c.IsDeleted).HasData(new UserFeature[]
            {
                new UserFeature { Id=1, Name="FullName",UserId = 1,FeatureId = 1,Value = "Administrator" },
                new UserFeature { Id=2, Name="e-Mail",UserId = 1,FeatureId = 5,Value = "Admin@MyServices.com" },
                new UserFeature { Id=3, Name="MobileNumber",UserId = 1,FeatureId = 6,Value = "+9898765432101" },

                new UserFeature { Id=4, Name="FullName",UserId = 2,FeatureId = 1,Value = "Support User" },
                new UserFeature { Id=5, Name="e-Mail",UserId = 2,FeatureId = 5,Value = "Support@MyServices.com" },
                new UserFeature { Id=6, Name="MobileNumber",UserId = 2,FeatureId = 6,Value = "+9898765432102" },
            });
            modelBuilder.Entity<UserFeature>().HasIndex(c => new { c.User, c.FeatureId });


            modelBuilder.Entity<Feature>().HasQueryFilter(c => !c.IsDeleted).HasData(new Feature[]
            {
                new Feature { Id= 1,Name = "FullName" },
                new Feature { Id= 2,Name = "NationalCode" },
                new Feature { Id= 3,Name = "Address" },
                new Feature { Id = 4,Name = "Gender" },
                new Feature { Id= 5,Name = "e-Mail" },
                new Feature { Id= 6,Name = "MobileNumber" },
            });

        }

    }
}