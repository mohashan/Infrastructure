using Infrastructure.BaseDomain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infrastructure.Messenger.Models
{
    public class MessengerDbContext : ApplicationDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            Guid channel1 = Guid.NewGuid();
            Guid channel2 = Guid.NewGuid();
            modelBuilder.Entity<Channel>().HasQueryFilter(c => !c.IsDeleted).HasData(new Channel[]
            {
                new Channel
                {
                    Id=channel1,
                    Name = "SMS",
                    FeatureId = Guid.NewGuid(), // Must replace with valid guid
                    EndPoint = "https://sms.MyServices.com/Send",
                    HttpRequestBody = @"{to:'@to',text:'@text'}"
                },
                new Channel
                {
                    Id = channel2,
                    Name = "e-Mail" ,
                    FeatureId = Guid.NewGuid(), // Must replace with valid guid
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