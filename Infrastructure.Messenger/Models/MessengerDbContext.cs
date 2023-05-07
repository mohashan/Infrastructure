using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infrastructure.Messenger.Models
{
    public class MessengerDbContext:DbContext
    {


        public MessengerDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<ContactType> ContactTypes { get; set; } = null!;
        public DbSet<Features> Features { get; set; } = null!;
        public DbSet<ContactFeatures> ContactFeatures { get; set; } = null!;
        public DbSet<Channels> Channels { get; set; } = null!;



    }
}