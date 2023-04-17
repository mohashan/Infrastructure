using Infrastructure.LiskShortener.Models;
using Microsoft.EntityFrameworkCore;

public class LinkShortenerDbContext:DbContext
{

        public LinkShortenerDbContext(DbContextOptions options) : base(options) { }
        public DbSet<ShortLink> ShortLink { get; set; } = null!;
    
}