using AspNetCoreRateLimit;
using Infrastructure.LinkShortener.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LinkShortenerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    // dotnet ef migrations add InitialCreate --output-dir Migrations
    // Commit Migration to have db on local sql server, plus some sample data
    var dataContext = scope.ServiceProvider.GetRequiredService<LinkShortenerDbContext>();
    dataContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("{shortCode}", (LinkShortenerDbContext db, HttpResponse response, string shortCode) =>
{
    var shortLink = db.ShortLink.FirstOrDefault(c => c.ShortCode == shortCode);
    if (shortLink != null)
    {
        return Results.Redirect(shortLink.OriginalUrl);
    }

    return Results.NotFound();
});

app.MapPost("/", (LinkShortenerDbContext db, ShortLinkRequestDTO link) => {
    if(!Infrastructure.BaseTools.UriTools.IsValidUri(link.OriginalUrl))
        return Results.BadRequest(link.OriginalUrl);
    ShortLink shortLink = new();
    shortLink.OriginalUrl = link.OriginalUrl.Trim('/');
    shortLink.ShortCode = ShorterTools.GenerateShortCode();

    db.ShortLink.Add(shortLink);
    try
    {
        db.SaveChanges();
        return Results.Created("/", shortLink);
    }
    catch (Exception)
    {
        return Results.BadRequest(shortLink);
    }
});

app.Run();

internal class ShorterTools
{
    const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    internal static string GenerateShortCode()
    {
        var random = new Random();
        return new string(Enumerable.Range(1, 6).Select(_ => chars[random.Next(chars.Length)]).ToArray());
    }
}