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
        public static void RegisterEntitiesForDbSet<T,TDto,TReadDto>(ModelBuilder modelBuilder, string assemblyName) 
            where T : BaseEntity<T,TDto,TReadDto>
            where TDto : BaseDto<T,TDto,TReadDto>
            where TReadDto : BaseReadDto<T,TDto,TReadDto>
        {
            var entityMethod = typeof(ModelBuilder).GetMethod("Entity", new[] { typeof(string) });
            var types = Assembly.Load(assemblyName).GetTypes().Where(x => x.BaseType != null && x.BaseType.Name == typeof(T).Name);
            foreach (var type in types)
            {
                entityMethod?.MakeGenericMethod(type).Invoke(modelBuilder, new object[] { });
            }
        }

        public DbContext CreateDbContext()
        {
            throw new NotImplementedException();
        }
    }
}
