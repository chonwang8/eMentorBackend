using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<GetChannelByTopicIdViewModel> GetChannelByTopicId(List<Guid> TopicIds)
        {
            List<GetChannelByTopicIdViewModel> result = new List<GetChannelByTopicIdViewModel>();

            foreach(var topicId in TopicIds)
            {
                IEnumerable<Channel> channels = _uow.GetRepository<Channel>().GetAll()
                    .Where(c => c.TopicId == topicId).Include(c => c.Topic).Include(c => c.Mentor.User);
                if (channels != null)
                {
                    foreach (var channel in channels)
                    {
                        result.Add(new GetChannelByTopicIdViewModel
                        {
                            ChannelId = channel.ChannelId,
                            TopicId = channel.TopicId,
                            TopicName = channel.Topic.TopicName,
                            MentorId = channel.MentorId,
                            MentorName = channel.Mentor.User.Fullname
                        });
                    }                   
                }
            }
            return result;
        }
    }
}
