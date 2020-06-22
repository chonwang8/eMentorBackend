using Data.Entities;
using Domain.DTO;
using Domain.ViewModels;
using Domain.ViewModels.ChannelViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface IChannelService
    {
        public List<GetChannelViewModel> GetAllChannel(GetAllDTO request);

        public GetChannelViewModel GetChannelById(Guid ChannelId);

        public List<GetChannelByTopicIdViewModel> GetChannelByTopicId(List<Guid> TopicIds);

        public bool DeleteChannelById(Guid ChannelId);

        public bool UpdateChannelById(UpdateChannelDTO channel);

        public bool CreateChannel(CreateChannelDTO channel);


        //  Wang - hot fix
        public int Count(Guid channelId);
        public ChannelSubsCountViewModel GetChannelSubCount(Guid channelId);
    }
}
