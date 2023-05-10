using Infrastructure.Messenger.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Messenger.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly MessengerDbContext ctx;

        public ContactController(MessengerDbContext context)
        {
            this.ctx = context;
        }
        [HttpGet]
        public async Task<ActionResult> Index(int count = 10, int pageNumber = 1)
        {
            return Ok((await ctx.Contacts.Skip(count * (pageNumber - 1)).Take(count).AsNoTracking().Select(c=>c.GetReadDto()).ToListAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Details(int id)
        {
            var item = await ctx.Contacts.FindAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item.GetReadDto());
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ContactDto createContact)
        {
            if (createContact == null)
            {
                return BadRequest();
            }

            var contact = new Contact().GetContact(createContact);
            try
            {
                ctx.Contacts.Add(contact);
                await ctx.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Create new contact failed");
            }
            


            return CreatedAtAction(nameof(Details),
                new {  id = contact.Id },contact.GetReadDto());
        }

        [HttpPatch("{id:int}")]
        public ActionResult<Contact> PartiallyUpdate(int id, [FromBody] JsonPatchDocument<ContactDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var existingDto= ctx.Contacts.Find(id)?.GetDto();

            if (existingDto == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(existingDto);

            var contact = new Contact().GetContact(existingDto);

            TryValidateModel(contact);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ctx.Contacts.Update(contact);

            try
            {

                ctx.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Update a contact failed on save");
            }


            return Ok(contact);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Remove(int id)
        {
            var contact = ctx.Contacts.Find(id);

            if (contact == null)
            {
                return NotFound();
            }

            contact.IsDeleted = true;
            contact.DeleteDate = DateTime.Now;
            ctx.Entry<Contact>(contact).State = EntityState.Modified;

            try
            {

                ctx.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Update a contact failed on save");
            }


            return NoContent();
        }

        [HttpPut("{id:int}")]
        public ActionResult<Contact> Update(int id, [FromBody] ContactDto contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }

            var existingItem = ctx.Contacts.Find(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.GetContact(contact);

            ctx.Entry<Contact>(existingItem).State = EntityState.Modified;
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Update a contact failed on save");
            }


            return Ok(existingItem.GetDto());
        }
    }
}
