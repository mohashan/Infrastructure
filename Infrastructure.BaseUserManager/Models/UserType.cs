using Infrastructure.BaseDomain;

namespace Infrastructure.BaseUserManager.Models
{
    public class UserType : BaseEntity<UserType, UserTypeCreateDto, UserTypeReadDto,UserTypeListDto>
    {


    }

    public class UserTypeCreateDto : BaseCreateDto<UserType, UserTypeCreateDto, UserTypeReadDto, UserTypeListDto>
    {

    }

    public class UserTypeReadDto : BaseReadDto<UserType, UserTypeCreateDto, UserTypeReadDto, UserTypeListDto>
    {

    }

    public class UserTypeListDto:BaseListDto<UserType, UserTypeCreateDto, UserTypeReadDto, UserTypeListDto>
    {

    }
}
