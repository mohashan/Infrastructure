using AutoMapper;
using Infrastructure.BaseTools;
using Infrastructure.Messenger.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<MessengerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IHttpRequester, HttpRequester>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // dotnet ef migrations add InitialCreate --output-dir Migrations
    // Commit Migration to have db on local sql server, plus some sample data
    var dataContext = scope.ServiceProvider.GetRequiredService<MessengerDbContext>();
    dataContext.Database.Migrate();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // To Check Exception Page >> "swagger/v1/swagger.json"
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
