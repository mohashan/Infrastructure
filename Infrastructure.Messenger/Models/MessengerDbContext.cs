using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infrastructure.Messenger.Models
{
    public class MessengerDbContext : DbContext
    {


        public MessengerDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<ContactType> ContactTypes { get; set; } = null!;
        public DbSet<Feature> Features { get; set; } = null!;
        public DbSet<ContactFeatures> ContactFeatures { get; set; } = null!;
        public DbSet<Channels> Channels { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContactType>().HasQueryFilter(c=>!c.IsDeleted).HasData(new ContactType[]
            {
                new ContactType { Id=1,Name = "People" },
                new ContactType { Id = 2,Name = "Service" },
                new ContactType { Id= 3,Name = "Device" },
            });

            modelBuilder.Entity<Contact>().HasQueryFilter(c => !c.IsDeleted).HasData(new Contact[]
            {
                new Contact { Id=1,Name = "Admin",TypeId = 1 },
                new Contact { Id = 2,Name = "SupportUser",TypeId = 1 },
            });

            modelBuilder.Entity<ContactFeatures>().HasQueryFilter(c => !c.IsDeleted).HasData(new ContactFeatures[]
            {
                new ContactFeatures { Id=1,ContactId = 1,FeatureId = 1,Value = "Administrator" },
                new ContactFeatures { Id=2,ContactId = 1,FeatureId = 5,Value = "Admin@MyServices.com" },
                new ContactFeatures { Id=3,ContactId = 1,FeatureId = 6,Value = "+9898765432101" },

                new ContactFeatures { Id=4,ContactId = 2,FeatureId = 1,Value = "Support User" },
                new ContactFeatures { Id=5,ContactId = 2,FeatureId = 5,Value = "Support@MyServices.com" },
                new ContactFeatures { Id=6,ContactId = 2,FeatureId = 6,Value = "+9898765432102" },
            });

            modelBuilder.Entity<Feature>().HasQueryFilter(c => !c.IsDeleted).HasData(new Feature[]
            {
                new Feature { Id= 1,Name = "FullName" },
                new Feature { Id= 2,Name = "NationalCode" },
                new Feature { Id= 3,Name = "Address" },
                new Feature { Id = 4,Name = "Gender" },
                new Feature { Id= 5,Name = "e-Mail" },
                new Feature { Id= 6,Name = "MobileNumber" },
            });

            modelBuilder.Entity<Channels>().HasQueryFilter(c => !c.IsDeleted).HasData(new Channels[]
            {
                new Channels 
                { 
                    Id=1,
                    Name = "SMS", 
                    FeatureId = 6,
                    EndPoint = "https://sms.MyServices.com/Send",
                    ChannelRequestType=ChannelRequestType.POST,
                    Body = @"{to:@MobileNumber,text:@Text}"
                },
                new Channels 
                { 
                    Id = 2,
                    Name = "e-Mail" ,
                    FeatureId = 5,
                    EndPoint = "https://EMail.MyServices.com/Send",
                    ChannelRequestType = ChannelRequestType.POST
                },
            });
        }

    }
}