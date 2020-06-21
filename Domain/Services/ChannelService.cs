using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using Domain.ViewModels.ChannelViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
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

            foreach (var topicId in TopicIds)
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

        public ChannelSubsCountViewModel GetChannelSubCount(Guid channelId)
        {
            IEnumerable<ChannelViewModel> list = _uow
                .GetRepository<Channel>()
                .GetAll()
                .Include(c => c.Topic)
                .Include(c => c.Mentor)
                .Include(c => c.Subscription)
                .Select(c => new ChannelViewModel
                {
                    ChannelId = c.ChannelId,
                    MentorName = c.Mentor.User.Email,
                    TopicName = c.Topic.TopicName,
                    Subscription = c.Subscription
                });

            ChannelViewModel channel = list
                .FirstOrDefault(i => i.ChannelId == channelId);

            ChannelSubsCountViewModel channelSubCount = new ChannelSubsCountViewModel
            {
                ChannelId = channel.ChannelId,
                MentorName = channel.MentorName
            };


            foreach (Subscription s in channel.Subscription)
            {
                int currentMonth = int.Parse(s.TimeSubscripted.Month.ToString()) - 1;
                var count = (int) channelSubCount.MonthSubsCount[currentMonth];
                channelSubCount.MonthSubsCount[currentMonth] = count + 1;
            }

            return channelSubCount;
        }

        //public ChannelSubsCountViewModel CountSubscriptionsByYear(Guid channelId)
        //{
        //    if (channelId == null)
        //    {
        //        return null;
        //    }

        //    var list = GetChannelInfo();
        //    var item = list.FirstOrDefault(i => i.ChannelId == channelId);

        //    ChannelSubsCountViewModel result = new ChannelSubsCountViewModel
        //    {
        //        ChannelId = channelId
        //    };

        //    foreach (Subscription sub in item.Subcription) 
        //    {

        //    }

        //    return result;
        //}

    }
}
