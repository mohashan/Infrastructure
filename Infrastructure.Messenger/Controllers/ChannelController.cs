using Infrastructure.BaseControllers;
using Infrastructure.BaseDataProvider.Repository;
using Infrastructure.BaseDomain;
using Infrastructure.Messenger.Models;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Messenger.Controllers
{
    public class ChannelController : GenericController<Channel, ChannelCreateDto, ChannelReadDto,ChannelListDto>
    {

        public ChannelController(MessengerDbContext ctx
            , AutoMapper.IConfigurationProvider cfg
            ,IBaseRepository<Channel, ChannelCreateDto, ChannelReadDto, ChannelListDto> repo) : base(ctx, cfg,repo)
        {
        }

    }
}
