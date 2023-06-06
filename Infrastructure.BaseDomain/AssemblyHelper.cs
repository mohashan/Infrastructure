using Microsoft.EntityFrameworkCore;

namespace Infrastructure.BaseDomain
{
    public static class AssemblyHelper
    {
       public static bool IsIEntityTypeConfiguration(this Type toCheck)
        {
            var baseClass = typeof(IEntityTypeConfiguration<>);
            if (baseClass == toCheck)
            {
                return false;
            }
            if (toCheck.BaseType?.GetGenericArguments().Count() != 1)
            {
                return false;
            }
            var to = toCheck;
            while (to != null && to != typeof(object))
            {
                var cur = to.IsGenericType ? to.GetGenericTypeDefinition() : to;
                if (baseClass == cur)
                {
                    return true;
                }
                to = to.BaseType;
            }
            return false;
        }

        public static bool IsEntity<T, TCreateDto, TReadDto, TListDto>(this Type toCheck)
        where T : BaseEntity<T, TCreateDto, TReadDto, TListDto>
    where TCreateDto : BaseCreateDto<T, TCreateDto, TReadDto, TListDto>
    where TReadDto : BaseReadDto<T, TCreateDto, TReadDto, TListDto>
    where TListDto : BaseListDto<T, TCreateDto, TReadDto, TListDto>
        {
            var baseEntity = typeof(BaseEntity<T, TCreateDto, TReadDto, TListDto>);
            if (baseEntity == toCheck)
            {
                return false;
            }
            if (toCheck.BaseType?.GetGenericArguments().Count() != 4)
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
}
