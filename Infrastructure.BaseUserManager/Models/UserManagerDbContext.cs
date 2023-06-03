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
            Guid Usertype1 = Guid.NewGuid();
            Guid Usertype2 = Guid.NewGuid();
            Guid Usertype3 = Guid.NewGuid();
            modelBuilder.Entity<UserType>().HasQueryFilter(c => !c.IsDeleted).HasData(new UserType[]
            {
                new UserType { Id=Usertype1,Name = "People" },
                new UserType { Id = Usertype2,Name = "Service" },
                new UserType { Id= Usertype3,Name = "Device" },
            });
            Guid groupId = Guid.NewGuid();
            modelBuilder.Entity<Group>().HasQueryFilter(c => !c.IsDeleted).HasData(new Group[]
            {
                new Group { Id=groupId,Name = "G1" },
            });
            Guid user1 = Guid.NewGuid();
            Guid user2 = Guid.NewGuid();
            modelBuilder.Entity<User>().HasQueryFilter(c => !c.IsDeleted).HasData(new User[]
            {
                new User { Id=Guid.NewGuid(),Name = "Admin",UserType = new UserType
                    {
                    Id = user1,
                    Name = "People",
                    }
                },
                new User { Id = user2,Name = "SupportUser"},
            });

            modelBuilder.Entity<UserGroup>().HasQueryFilter(c => !c.IsDeleted).HasData(new UserGroup[]
            {
                new UserGroup { Id=Guid.NewGuid(),Name = "Admin",UserId = user1,GroupId=groupId },
                new UserGroup { Id = Guid.NewGuid(),Name = "SupportUser",UserId = user2,GroupId = groupId },
            });
            modelBuilder.Entity<UserGroup>().HasIndex(c => new { c.UserId, c.GroupId });

            Guid feature1 = new Guid("15010239-CFB7-4831-B83E-142B912AFF3A");
            Guid feature2 = new Guid("A292472D-2605-4C36-80ED-F012A04A2953");
            Guid feature3 = new Guid("A292472D-2605-4C36-80ED-F012A04A2953");
            Guid feature4 = new Guid("1C71CC50-FF8B-4F9B-B38F-9068297FC093");
            Guid feature5 = new Guid("B0CAF7D1-9937-46A4-BB1A-DDD7E6F57F3C");
            Guid feature6 = new Guid("7EC91532-A479-4759-B5C9-21C3A0493769");
            modelBuilder.Entity<Feature>().HasQueryFilter(c => !c.IsDeleted).HasData(new Feature[]
            {
                new Feature { Id= feature1,Name = "FullName" },
                new Feature { Id= feature2,Name = "NationalCode" },
                new Feature { Id= feature3,Name = "Address" },
                new Feature { Id = feature4,Name = "Gender" },
                new Feature { Id= feature5,Name = "e-Mail" },
                new Feature { Id= feature6,Name = "MobileNumber" },
            });

            modelBuilder.Entity<UserFeature>().HasQueryFilter(c => !c.IsDeleted).HasData(new UserFeature[]
            {
                new UserFeature { Id= Guid.NewGuid(), Name="FullName",UserId = user1,FeatureId = feature1,Value = "Administrator" },
                new UserFeature { Id=Guid.NewGuid(), Name="e-Mail",UserId = user1,FeatureId = feature5,Value = "Admin@MyServices.com" },
                new UserFeature { Id=Guid.NewGuid(), Name="MobileNumber",UserId = user1,FeatureId = feature6,Value = "+9898765432101" },

                new UserFeature { Id=Guid.NewGuid(), Name="FullName",UserId = user2,FeatureId = feature1,Value = "Support User" },
                new UserFeature { Id=Guid.NewGuid(), Name="e-Mail",UserId = user2,FeatureId = feature5,Value = "Support@MyServices.com" },
                new UserFeature { Id=Guid.NewGuid(), Name="MobileNumber",UserId = user2,FeatureId = feature6,Value = "+9898765432102" },
            });
            modelBuilder.Entity<UserFeature>().HasIndex(c => new { c.User, c.FeatureId });


            

        }

    }
}