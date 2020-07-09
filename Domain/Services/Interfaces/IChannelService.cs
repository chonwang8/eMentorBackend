using Domain.DTO;
using Domain.DTO.QueryAttributesDtos;
using Domain.DTO.ResponseDtos;
using Domain.ViewModels.ChannelModels;
using System;
using System.Collections.Generic;

namespace Domain.Services.Interfaces
{
    public interface IChannelService
    {
        public ChannelResponseDto<ChannelViewModel> GetAll(PagingDto pagingRequest);
        public ChannelResponseDto<ChannelModel> GetById(string ChannelId);
        public ChannelResponseDto Insert(ChannelInsertModel channel);
        public ChannelResponseDto Update(ChannelUpdateModel channelUpdateModel);
        public ChannelResponseDto ChangeStatus(string channelId, bool status);
        public ChannelResponseDto Delete(string ChannelId);


        //  Keep
        public List<GetChannelByTopicIdViewModel> GetChannelByTopicId(List<Guid> TopicIds);

        //  Wang - hot fix
        public int Count(Guid channelId);
        public ChannelSubsCountViewModel GetChannelSubCount(Guid channelId);
    }
}
