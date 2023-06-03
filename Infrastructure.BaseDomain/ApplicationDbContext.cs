using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BaseDomain
{


    public class ApplicationDbContext : DbContext
    {
        
        public static void RegisterEntitiesForDbSet(ModelBuilder modelBuilder, string assemblyName)
        {
            var entityMethod = typeof(ModelBuilder).GetMethod("Entity", new[] { typeof(string) });
            var types = Assembly.Load(assemblyName).GetTypes().Where(x => x.BaseType != null && x.BaseType.Name == typeof(BaseEntity<,,,>).Name);
            foreach (var type in types)
            {
                entityMethod?.MakeGenericMethod(type).Invoke(modelBuilder, new object[] { });
            }
        }

        
    }
}
