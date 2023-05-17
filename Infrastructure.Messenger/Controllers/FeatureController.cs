using AutoMapper;
using Infrastructure.Messenger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Messenger.Controllers
{
    public class FeatureController:GenericController<Feature,FeatureDto,FeatureReadDto>
    {

        public FeatureController(MessengerDbContext ctx,AutoMapper.IConfigurationProvider cfg):base(ctx, cfg)
        {
        }
        [HttpGet("{FeatureId:int}/[action]/{ContactId:int}")]
        public async Task<ActionResult> GetByContactId(int FeatureId, int ContactId)
        {
            var result = (await ctx.ContactFeatures.Include(c=>c.Feature).
                                FirstOrDefaultAsync(c=>c.FeatureId == FeatureId && c.ContactId == ContactId))?
                                .GetReadDto(mapper);

            if(result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("{FeatureId:int}/[action]/{ContactId:int}")]
        public async Task<ActionResult> SetByContactId(int FeatureId, int ContactId, [FromBody] ContactFeatureDto dto)
        {
            var existEntity = await ctx.ContactFeatures.Include(c => c.Feature).
                                FirstOrDefaultAsync(c => c.FeatureId == FeatureId && c.ContactId == ContactId);
            ContactFeature entity = new ContactFeature().GetEntity(dto,mapper);
            if (existEntity == null)
            {
                existEntity = new ContactFeature
                {
                    ContactId = ContactId,
                    FeatureId = FeatureId,
                    Value = dto.Value,
                };
                ctx.ContactFeatures.Add(existEntity);
            }
            else
            {
                existEntity.Value = dto.Value;
                ctx.Entry(existEntity).State = EntityState.Modified;
            }
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest("Could not Save changes");
            }
            return CreatedAtAction(nameof(GetByContactId),new { FeatureId, ContactId }, existEntity.GetReadDto(mapper));
        }
    }
}
