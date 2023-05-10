using AutoMapper;
using System;

namespace Infrastructure.Messenger.Models
{
    public class ContactType:BaseEntity<ContactType,ContactTypeDto,ContactTypeReadDto>
    {

        public string Name { get; set; }

        public ContactType GetEntity(ContactTypeDto contact)
        {
            Name = contact.Name;
            return this;
        }

        public ContactTypeDto GetDto()
        {
            return new ContactTypeDto(Name);
        }

        public ContactTypeReadDto GetReadDto()
        {
            return new ContactTypeReadDto(Id, Name);
        }
    }

    public record ContactTypeDto(string Name):BaseDto;

    public record ContactTypeReadDto(int Id, string Name):BaseReadDto(Id);
}
