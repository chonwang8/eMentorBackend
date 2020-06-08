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
    }
}
