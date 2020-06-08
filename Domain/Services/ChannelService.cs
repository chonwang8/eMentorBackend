using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class ChannelService : IChannelService
    {
        #region Classes and Constructor

        protected readonly IUnitOfWork _uow;

        public ChannelService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        #endregion Classes and Constructor
        public List<GetChannelViewModel> GetAllChannel()
        {
            List<GetChannelViewModel> result = new List<GetChannelViewModel>();
            IEnumerable<Channel> channels = _uow.GetRepository<Channel>().GetAll();
            foreach (var channel in channels)
            {
                result.Add(new GetChannelViewModel
                {
                    ChannelId = channel.ChannelId,
                    MentorId = channel.MentorId,
                    TopicId = channel.TopicId
                });
            }
            return result;
        }

        public GetChannelViewModel GetChannelById(Guid ChannelId)
        {
            Channel channel = _uow.GetRepository<Channel>().Get(ChannelId);
            if (channel != null)
            {
                GetChannelViewModel result = new GetChannelViewModel
                {
                    ChannelId = channel.ChannelId,
                    MentorId = channel.MentorId,
                    TopicId = channel.TopicId
                };
                return result;
            }
            return null;
        }
    }
}
