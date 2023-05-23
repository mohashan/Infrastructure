using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BaseDomain
{
    public static class AssemblyHelper
    {
        public static bool IsEntity(this Type toCheck)
        {
            var baseEntity = typeof(BaseEntity<,,>);
            if (baseEntity == toCheck)
            {
                return false;
            }
            if(toCheck.BaseType?.GetGenericArguments().Count() != 3)
            {
                return false;
            }
            var to = toCheck;
            while (to != null && to != typeof(object))
            {
                var cur = to.IsGenericType ? to.GetGenericTypeDefinition() : to;
                if (baseEntity == cur)
                {
                    return true;
                }
                to = to.BaseType;
            }
            return false;
        }
    }

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            Assembly.GetExecutingAssembly().GetTypes().Where(c=>c.IsEntity()).ToList().ForEach(type =>
            {

                    var baseType = type.BaseType;
                    var entityType = baseType?.GetGenericArguments()[0];
                    var dtoType = baseType?.GetGenericArguments()[1];
                    var readDtoType = baseType?.GetGenericArguments()[2];

                    CreateMap(entityType, readDtoType).ReverseMap();
                    CreateMap(readDtoType, dtoType).ReverseMap();
                    CreateMap(dtoType, entityType).ReverseMap();
            });
        }
    }


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
                entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] { });
            }
        }

    }
}
