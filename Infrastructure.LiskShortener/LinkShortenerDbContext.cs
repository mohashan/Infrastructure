using Infrastructure.LinkShortener.Models;
using Microsoft.EntityFrameworkCore;

public class LinkShortenerDbContext:DbContext
{

        public LinkShortenerDbContext(DbContextOptions options) : base(options) { }
        public DbSet<ShortLink> ShortLink { get; set; } = null!;
    
}