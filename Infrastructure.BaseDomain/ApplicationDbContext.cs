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
        public static void RegisterEntitiesForDbSet<T,TCreateDto,TReadDto,TListDto>(ModelBuilder modelBuilder, string assemblyName)
            where T : BaseEntity<T, TCreateDto, TReadDto, TListDto>
    where TCreateDto : BaseCreateDto<T, TCreateDto, TReadDto, TListDto>
    where TReadDto : BaseReadDto<T, TCreateDto, TReadDto, TListDto>
    where TListDto : BaseListDto<T, TCreateDto, TReadDto, TListDto>
        {
            var entityMethod = typeof(ModelBuilder).GetMethod("Entity", new[] { typeof(string) });
            var types = Assembly.Load(assemblyName).GetTypes().Where(x => x.BaseType != null && x.BaseType.Name == typeof(T).Name);
            foreach (var type in types)
            {
                entityMethod?.MakeGenericMethod(type).Invoke(modelBuilder, new object[] { });
            }
        }

        
    }
}
