using Infrastructure.BaseControllers;
using Infrastructure.BaseDomain;
using Infrastructure.Messenger.Models;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Messenger.Controllers
{
    public class ChannelController : GenericController<Channel, ChannelCreateDto, ChannelReadDto,ChannelListDto>
    {

        public ChannelController(MessengerDbContext ctx, AutoMapper.IConfigurationProvider cfg) : base(ctx, cfg)
        {
        }

    }
}
