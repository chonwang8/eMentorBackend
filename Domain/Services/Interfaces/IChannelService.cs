using Domain.DTO;
using Domain.ViewModels.ChannelModels;
using System;
using System.Collections.Generic;

namespace Domain.Services.Interfaces
{
    public interface IChannelService
    {
        public List<ChannelViewModel> GetAll(GetAllDTO request);

        public ChannelModel GetById(Guid ChannelId);

        public bool Insert(ChannelInsertModel channel);

        public bool Update(UpdateChannelDTO channel);

        public bool Delete(Guid ChannelId);

        public List<GetChannelByTopicIdViewModel> GetChannelByTopicId(List<Guid> TopicIds);

        //  Wang - hot fix
        public int Count(Guid channelId);
        public ChannelSubsCountViewModel GetChannelSubCount(Guid channelId);
    }
}
