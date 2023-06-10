using Infrastructure.BaseUserManager.IRepository;
using Infrastructure.BaseUserManager.Models;
using Infrastructure.BaseUserManager.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BaseUserManager.RegisterIOC;

public static class UserManagerIoc
{
    public static IServiceCollection RegisterUserManager(this IServiceCollection services,string connectionString)
    {
        services.AddDbContext<UserManagerDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        return services.AddScoped<IGroupRepositiry, GroupRepositiry>()
            .AddScoped<IUserRepositiry, UserRepository>();

    }
}
