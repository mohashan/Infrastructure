using AutoMapper;
using Infrastructure.BaseDomain;
using System;

namespace Infrastructure.Messenger.Models
{
    public class ContactType:BaseEntity<ContactType,ContactTypeDto,ContactTypeReadDto>
    {


    }

    public class ContactTypeDto:BaseDto<ContactType, ContactTypeDto, ContactTypeReadDto>
    {

    }

    public class ContactTypeReadDto:BaseReadDto<ContactType, ContactTypeDto, ContactTypeReadDto>
    {

    }
}
