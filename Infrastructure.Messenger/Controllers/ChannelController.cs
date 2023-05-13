using Infrastructure.Messenger.Models;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Messenger.Controllers
{
    public class ChannelController : GenericController<Channel, ChannelDto, ChannelReadDto>
    {

        public ChannelController(MessengerDbContext ctx, AutoMapper.IConfigurationProvider cfg) : base(ctx, cfg)
        {
        }

    }
}
