using Infrastructure.BaseDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Infrastructure.Messenger.Models
{
    public class MessengerDbContext : ApplicationDbContext
    {
        public MessengerDbContext(DbContextOptions options):base(options)
        {
            //RegisterEntitiesForDbSet(Assembly.GetExecutingAssembly());

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            Guid feature1 = new Guid("15010239-CFB7-4831-B83E-142B912AFF3A");
            Guid feature2 = new Guid("A292472D-2605-4C36-80ED-F012A04A2953");
            Guid feature3 = new Guid("A292472D-2605-4C36-80ED-F012A04A2953");
            Guid feature4 = new Guid("1C71CC50-FF8B-4F9B-B38F-9068297FC093");
            Guid feature5 = new Guid("B0CAF7D1-9937-46A4-BB1A-DDD7E6F57F3C");
            Guid feature6 = new Guid("7EC91532-A479-4759-B5C9-21C3A0493769");

            Guid channel1 = Guid.NewGuid();
            Guid channel2 = Guid.NewGuid();
            modelBuilder.Entity<Channel>().HasQueryFilter(c => !c.IsDeleted).HasData(new Channel[]
            {
                new Channel
                {
                    Id=channel1,
                    Name = "SMS",
                    FeatureId = feature6,
                    EndPoint = "https://sms.MyServices.com/Send",
                    HttpRequestBody = @"{to:'@to',text:'@text'}"
                },
                new Channel
                {
                    Id = channel2,
                    Name = "e-Mail" ,
                    FeatureId = feature5,
                    EndPoint = "https://EMail.MyServices.com/Send",
                    HttpRequestBody = @"{to:'@to',text:'@text'}"
                },
            });
            Guid template1 = Guid.NewGuid();
            Guid template2 = Guid.NewGuid();
            modelBuilder.Entity<Template>().HasQueryFilter(c => !c.IsDeleted).HasData(new Template[]
            {
                new Template
                {
                    Id=template1,
                    Name = "Server_Monitoring",
                    Body = @"Server @param0 has a problem: @param1"
                },
                new Template
                {
                    Id=template2,
                    Name = "Service_Monitoring",
                    Body = @"Service @param0 has a problem: @param1"
                },
            });
        }

    }
}