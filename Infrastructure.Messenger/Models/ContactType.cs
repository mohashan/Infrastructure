using AutoMapper;
using System;

namespace Infrastructure.Messenger.Models
{
    public class ContactType:BaseEntity<ContactType,ContactTypeDto,ContactTypeReadDto>
    {


    }

    public record ContactTypeDto(string? title):BaseDto(title);

    public record ContactTypeReadDto(int Id, string title,DateTime InsertDate):BaseReadDto(Id,title, InsertDate);
}
