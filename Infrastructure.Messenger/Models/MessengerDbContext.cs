using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infrastructure.Messenger.Models
{
    public class MessengerDbContext : DbContext
    {
        public MessengerDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<ContactGroup> ContactGroups { get; set; } = null!;
        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<ContactType> ContactTypes { get; set; } = null!;
        public DbSet<Feature> Features { get; set; } = null!;
        public DbSet<ContactFeature> ContactFeatures { get; set; } = null!;
        public DbSet<Channel> Channels { get; set; } = null!;
        public DbSet<Template> Templates { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<ContactType>().HasQueryFilter(c=>!c.IsDeleted).HasData(new ContactType[]
            {
                new ContactType { Id=1,Title = "People" },
                new ContactType { Id = 2,Title = "Service" },
                new ContactType { Id= 3,Title = "Device" },
            });

            modelBuilder.Entity<Group>().HasQueryFilter(c => !c.IsDeleted).HasData(new Group[]
            {
                new Group { Id=1,Title = "G1" },
            });

            modelBuilder.Entity<Contact>().HasQueryFilter(c => !c.IsDeleted).HasData(new Contact[]
            {
                new Contact { Id=1,Title = "Admin",ContactTypeId = 1 },
                new Contact { Id = 2,Title = "SupportUser",ContactTypeId = 1 },
            });

            modelBuilder.Entity<ContactGroup>().HasQueryFilter(c => !c.IsDeleted).HasData(new ContactGroup[]
            {
                new ContactGroup { Id=1,Title = "Admin",ContactId = 1,GroupId=1 },
                new ContactGroup { Id = 2,Title = "SupportUser",ContactId = 2,GroupId = 1 },
            });
            modelBuilder.Entity<ContactGroup>().HasIndex(c=>new { c.ContactId,c.GroupId}).IsUnique();

            modelBuilder.Entity<ContactFeature>().HasQueryFilter(c => !c.IsDeleted).HasData(new ContactFeature[]
            {
                new ContactFeature { Id=1, Title="FullName",ContactId = 1,FeatureId = 1,Value = "Administrator" },
                new ContactFeature { Id=2, Title="e-Mail",ContactId = 1,FeatureId = 5,Value = "Admin@MyServices.com" },
                new ContactFeature { Id=3, Title="MobileNumber",ContactId = 1,FeatureId = 6,Value = "+9898765432101" },

                new ContactFeature { Id=4, Title="FullName",ContactId = 2,FeatureId = 1,Value = "Support User" },
                new ContactFeature { Id=5, Title="e-Mail",ContactId = 2,FeatureId = 5,Value = "Support@MyServices.com" },
                new ContactFeature { Id=6, Title="MobileNumber",ContactId = 2,FeatureId = 6,Value = "+9898765432102" },
            });
            modelBuilder.Entity<ContactFeature>().HasIndex(c => new { c.ContactId, c.FeatureId }).IsUnique();


            modelBuilder.Entity<Feature>().HasQueryFilter(c => !c.IsDeleted).HasData(new Feature[]
            {
                new Feature { Id= 1,Title = "FullName" },
                new Feature { Id= 2,Title = "NationalCode" },
                new Feature { Id= 3,Title = "Address" },
                new Feature { Id = 4,Title = "Gender" },
                new Feature { Id= 5,Title = "e-Mail" },
                new Feature { Id= 6,Title = "MobileNumber" },
            });

            modelBuilder.Entity<Channel>().HasQueryFilter(c => !c.IsDeleted).HasData(new Channel[]
            {
                new Channel 
                { 
                    Id=1,
                    Title = "SMS", 
                    FeatureId = 6,
                    EndPoint = "https://sms.MyServices.com/Send",
                    HttpRequestBody = @"{to:'@to',text:'@text'}"
                },
                new Channel 
                { 
                    Id = 2,
                    Title = "e-Mail" ,
                    FeatureId = 5,
                    EndPoint = "https://EMail.MyServices.com/Send",
                    HttpRequestBody = @"{to:'@to',text:'@text'}"
                },
            });

            modelBuilder.Entity<Template>().HasQueryFilter(c => !c.IsDeleted).HasData(new Template[]
            {
                new Template
                {
                    Id=1,
                    Title = "Server_Monitoring",
                    Body = @"Server @param0 has a problem: @param1"
                },
                new Template
                {
                    Id=2,
                    Title = "Service_Monitoring",
                    Body = @"Service @param0 has a problem: @param1"
                },
            });
        }

    }
}