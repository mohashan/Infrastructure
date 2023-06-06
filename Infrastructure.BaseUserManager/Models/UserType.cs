using Infrastructure.BaseDomain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.BaseUserManager.Models
{
    public class UserType : BaseEntity<UserType, UserTypeCreateDto, UserTypeReadDto,UserTypeListDto>
    {


    }

    class UserTypeEntityConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
        }
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
