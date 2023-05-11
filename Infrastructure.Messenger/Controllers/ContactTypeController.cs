using Infrastructure.Messenger.Models;

namespace Infrastructure.Messenger.Controllers
{
    public class ContactTypeController : GenericController<ContactType, ContactTypeDto, ContactTypeReadDto>
    {
        public ContactTypeController(MessengerDbContext ctx, AutoMapper.IConfigurationProvider cfg) : base(ctx, cfg)
        {
        }
    }
}
