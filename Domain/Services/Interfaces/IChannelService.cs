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
        public BaseResponseDto<ChannelViewModel> GetAll();
        public BaseResponseDto<ChannelModel> GetById(string ChannelId);
        public BaseResponseDto Insert(ChannelInsertModel channel);
        public BaseResponseDto Update(ChannelUpdateModel channelUpdateModel);
        public BaseResponseDto ChangeStatus(string channelId, bool status);
        public BaseResponseDto Delete(string ChannelId);


        //  Keep
        public List<GetChannelByTopicIdViewModel> GetChannelByTopicId(List<Guid> TopicIds);

        //  Wang - hot fix
        public int Count(Guid channelId);
        public ChannelSubsCountViewModel GetChannelSubCount(string channelId);
    }
}
