namespace Infrastructure.BaseDomain
{
    public static class AssemblyHelper
    {
        public static bool IsEntity<T,TDto,TReadDto>(this Type toCheck)
        where T : BaseEntity<T, TDto, TReadDto>
        where TDto : BaseDto<T, TDto, TReadDto>
        where TReadDto : BaseReadDto<T, TDto, TReadDto>
        {
            var baseEntity = typeof(BaseEntity<T,TDto,TReadDto>);
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
}
