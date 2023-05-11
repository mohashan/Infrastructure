using Infrastructure.Messenger.Models;

namespace Infrastructure.Messenger.Controllers
{
    public class ChannelController : GenericController<Channel, ChannelDto, ChannelReadDto>
    {

        public ChannelController(MessengerDbContext ctx, AutoMapper.IConfigurationProvider cfg) : base(ctx, cfg)
        {
        }

    }
}
