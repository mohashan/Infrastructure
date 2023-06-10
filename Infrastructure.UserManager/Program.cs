using Infrastructure.BaseDataProvider.Repository;
using Infrastructure.BaseDomain;
using Infrastructure.BaseUserManager.Models;
using Infrastructure.BaseUserManager.RegisterIOC;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.UserManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.RegisterUserManager(builder.Configuration.GetConnectionString("SqlServer"));


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                // dotnet ef migrations add InitialCreate --output-dir Migrations
                // Commit Migration to have db on local sql server, plus some sample data
                //var dataContext = scope.ServiceProvider.GetRequiredService<UserManagerDbContext>();
                //dataContext.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}