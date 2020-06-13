﻿using Data.Entities;
using Domain.DTO;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface IChannelService
    {
        public List<GetChannelViewModel> GetAllChannel();

        public GetChannelViewModel GetChannelById(Guid ChannelId);

        public List<GetChannelByTopicIdViewModel> GetChannelByTopicId(List<Guid> TopicIds);

        public bool DeleteChannelById(Guid ChannelId);

        public bool UpdateChannelById(UpdateChannelModel channel);

        public bool CreateChannel(CreateChannelModel channel);
    }
}
