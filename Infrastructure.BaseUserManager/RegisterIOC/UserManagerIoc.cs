using Infrastructure.BaseUserManager.IRepository;
using Infrastructure.BaseUserManager.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BaseUserManager.RegisterIOC;

public static class UserManagerIoc
{
    public static IServiceCollection RegisterUserManager(this IServiceCollection services)
    {
        return services.AddTransient<IGroupRepositiry, GroupRepositiry>()
            .AddTransient<IUserRepositiry, UserRepository>();

    }
}
