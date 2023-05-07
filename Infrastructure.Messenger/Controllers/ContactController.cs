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
        public async Task<ActionResult> Index(int count, int pageNumber)
        {
            return Ok(await ctx.Contacts.Skip(count * (pageNumber - 1)).Take(count).AsNoTracking().ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> Details(int id)
        {
            var item = await ctx.Contacts.FindAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Contact contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }
            try
            {
                ctx.Contacts.Add(contact);
                ctx.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Create new contact failed");
            }
            


            return CreatedAtRoute(nameof(contact),
                new {  id = contact.Id });
        }

        [HttpPatch("{id:int}")]
        public ActionResult<Contact> PartiallyUpdate(int id, [FromBody] JsonPatchDocument<Contact> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var existingEntity = ctx.Contacts.Find(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(existingEntity);

            TryValidateModel(existingEntity);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = ctx.Contacts.Update(existingEntity);

            try
            {

                ctx.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Update a contact failed on save");
            }


            return Ok(existingEntity);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult RemoveFood(int id)
        {
            var contact = ctx.Contacts.Find(id);

            if (contact == null)
            {
                return NotFound();
            }

            contact.IsDeleted = true;

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

        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<Contact> Update(int id, [FromBody] Contact contact)
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

            ctx.Entry<Contact>(contact).State = EntityState.Modified;
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
    }
}
