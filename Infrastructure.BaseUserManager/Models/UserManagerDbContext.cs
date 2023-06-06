using Infrastructure.BaseDomain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;

namespace Infrastructure.BaseUserManager.Models
{
    public class UserManagerDbContext : ApplicationDbContext
    {
        public UserManagerDbContext(DbContextOptions options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(UserManagerDbContext)));


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

            Guid feature1 = Guid.NewGuid();
            Guid feature2 = Guid.NewGuid();
            Guid feature3 = Guid.NewGuid();
            Guid feature4 = Guid.NewGuid();
            Guid feature5 = Guid.NewGuid();
            Guid feature6 = Guid.NewGuid();
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