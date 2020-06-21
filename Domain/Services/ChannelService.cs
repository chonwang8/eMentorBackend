using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using Domain.ViewModels.ChannelViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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


        public List<GetChannelViewModel> GetAllChannel(GetAllDTO request)
        {
            List<GetChannelViewModel> result = new List<GetChannelViewModel>();
            IEnumerable<Channel> channels = _uow.GetRepository<Channel>().GetAll();
            channels = channels.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            if (request.IsAscending)
            {
                channels = channels.OrderBy(c => c.ChannelId);
            }
            else
            {
                channels = channels.OrderByDescending(c => c.ChannelId);
            }
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


        public bool DeleteChannelById(Guid ChannelId)
        {
            Channel channel = _uow.GetRepository<Channel>().Get(ChannelId);
            if (channel == null)
                return false;
            _uow.GetRepository<Channel>().Update(new Channel
            {
                ChannelId = channel.ChannelId,
                IsDisable = true
            });
            _uow.Commit();
            return true;
        }

        public bool UpdateChannelById(UpdateChannelDTO channel)
        {
            Channel oldChannel = _uow.GetRepository<Channel>().Get(channel.ChannelId);
            if (oldChannel == null)
                return false;
            _uow.GetRepository<Channel>().Update(new Channel
            {
                ChannelId = channel.ChannelId,
                TopicId = channel.TopicId,
                MentorId = channel.MentorId,
                IsDisable = oldChannel.IsDisable
            });
            _uow.Commit();
            return true;
        }

        public bool CreateChannel(CreateChannelDTO channel)
        {
            _uow.GetRepository<Channel>().Insert(new Channel
            {
                ChannelId = Guid.NewGuid(),
                TopicId = channel.TopicId,
                MentorId = channel.MentorId,
                IsDisable = false
            });
            _uow.Commit();
            return true;
        }



        //  Wang - hot fix
        public int Count(Guid channelId)
        {
            int count = _uow
                .GetRepository<Channel>()
                .GetAll()
                .Where(c => c.ChannelId == channelId)
                .Count();
            return count;
        }

        public IEnumerable<ChannelViewModel> GetChannelInfo(Guid channelId)
        {
            if(channelId == null)
            {
                return null;
            }

            IEnumerable<ChannelViewModel> channelInfo;
            channelInfo = _uow
                .GetRepository<Channel>()
                .GetAll()
                .Where(s => s.ChannelId == channelId)
                .Include(c => c.Subcription)
                .Include(c => c.Topic)
                .Include(c => c.Mentor)
                .Select(c => new ChannelViewModel
                {
                    ChannelId = c.ChannelId,
                    MentorName = c.Mentor.User.Email,
                    TopicName = c.Topic.TopicName,
                    Sharing = c.Sharing,
                    Subcription = c.Subcription
                });

            return channelInfo;
        }

        public void CountSubscriptionsByYear(Guid channelId)
        {
            var list = GetChannelInfo(channelId);


        }

    }
}
