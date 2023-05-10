using AutoMapper;
using Infrastructure.Messenger.Models;
using System.Net;
using System.Reflection;


namespace Infrastructure.Messenger
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            var AllTypes = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in AllTypes)
            {
                if (type == typeof(Contact))
                {

                }
                if (IsSubclassOfRawGeneric(typeof(BaseEntity<,,>), type) && typeof(BaseEntity<,,>) != type && type.BaseType?.GetGenericArguments().Count()==3)
                {
                    var baseType = type.BaseType;
                    var entityType = baseType?.GetGenericArguments()[0];
                    var dtoType = baseType?.GetGenericArguments()[1];
                    var readDtoType = baseType?.GetGenericArguments()[2];

                    CreateMap(entityType,readDtoType);
                    
                    CreateMap(dtoType,entityType);           
                
                }
            }
        }
        static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            var to = toCheck;
            while (to != null && to != typeof(object))
            {
                var cur = to.IsGenericType ? to.GetGenericTypeDefinition() : to;
                if (generic == cur)
                {
                    return true;
                }
                to = to.BaseType;
            }
            return false;
        }
    }
}
