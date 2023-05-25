using Infrastructure.BaseDomain;

namespace Infrastructure.BaseUserManager.Models
{
    public class UserType : BaseEntity<UserType, UserTypeDto, UserTypeReadDto>
    {


    }

    public class UserTypeDto : BaseDto<UserType, UserTypeDto, UserTypeReadDto>
    {

    }

    public class UserTypeReadDto : BaseReadDto<UserType, UserTypeDto, UserTypeReadDto>
    {

    }
}
