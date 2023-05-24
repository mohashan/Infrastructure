using Infrastructure.Messenger.Models;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Messenger.Controllers
{
    public class GroupController : GenericController<Group, GroupDto, GroupReadDto>
    {

        public GroupController(MessengerDbContext ctx, AutoMapper.IConfigurationProvider cfg) : base(ctx, cfg)
        {
        }

        [HttpPost("{id:int}/[action]/{contactId:int}")]
        public async Task<ActionResult> AddContactToGroup(int id, int contactId)
        {
            if(id == 0 || contactId == 0)
                return BadRequest();

            var IsContactInGroup = ctx.Set<ContactGroup>().Any(c=>c.Id == contactId && c.GroupId == id);
            if(IsContactInGroup)
                return BadRequest("Contact is already in this group");

            var contact = ctx.Set<Contact>().FirstOrDefault(c => c.Id == contactId);

            var contactGroup = new ContactGroup
            {
                ContactId = contactId,
                GroupId = id,
                Name = contact?.Name ?? string.Empty
            };

            ctx.Set<ContactGroup>().Add(contactGroup);
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest("Error On Add Contact to Group");
            }

            return CreatedAtAction(nameof(Details),
                new { id = contactGroup.Id }, contactGroup.GetReadDto(mapper));
        }
    }
}
