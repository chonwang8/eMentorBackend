using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.ViewModels.ChannelModels;
using Domain.ViewModels.MentorModels;
using Domain.ViewModels.TopicModels;
using Domain.ViewModels.UserModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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


        public List<ChannelViewModel> GetAll(GetAllDTO request)
        {
            List<ChannelViewModel> result = new List<ChannelViewModel>();
            IEnumerable<Channel> channels = _uow
                .GetRepository<Channel>()
                .GetAll()
                .Include(c => c.Mentor)
                .Include(c => c.Mentor.User)
                .Include(c => c.Topic);
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
                result.Add(new ChannelViewModel
                {
                    ChannelId = channel.ChannelId,
                    MentorName = channel.Mentor.User.Fullname,
                    TopicName = channel.Topic.TopicName
                });
            }
            return result;
        }

        public ChannelModel GetById(Guid ChannelId)
        {
            Channel channel = _uow.GetRepository<Channel>().Get(ChannelId);
            if (channel != null)
            {
                ChannelModel result = new ChannelModel
                {
                    ChannelId = channel.ChannelId,
                    Topic = new TopicViewModel
                    {
                        TopicId = channel.Topic.TopicId,
                        TopicName = channel.Topic.TopicName,
                        MajorId = channel.Topic.Major.MajorId,
                        CreatedBy = channel.Topic.CreatedBy
                    },
                    Mentor = new MentorViewModel
                    {
                        MentorId = channel.MentorId,
                        User = new UserViewModel
                        {
                            UserId = channel.Mentor.User.UserId,
                            Email = channel.Mentor.User.Email,
                            Phone = channel.Mentor.User.Phone,
                            Fullname = channel.Mentor.User.Fullname,
                            YearOfBirth = channel.Mentor.User.YearOfBirth,
                            AvatarUrl = channel.Mentor.User.AvatarUrl,
                            Balance = channel.Mentor.User.Balance,
                            Description = channel.Mentor.User.Description
                        }
                    },
                    Sharing = null,
                    Subscription = null,
                };
                return result;
            }
            return null;
        }


        public bool Insert(ChannelInsertModel channel)
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

        public bool Update(UpdateChannelDTO channel)
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

        public bool Delete(Guid ChannelId)
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




        //  Keep
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
            IEnumerable<ChannelDirtyModel> list = _uow
                .GetRepository<Channel>()
                .GetAll()
                .Include(c => c.Topic)
                .Include(c => c.Mentor)
                .Include(c => c.Subscription)
                .Select(c => new ChannelDirtyModel
                {
                    ChannelId = c.ChannelId,
                    MentorName = c.Mentor.User.Email,
                    TopicName = c.Topic.TopicName,
                    Subscription = c.Subscription
                });

            ChannelDirtyModel channel = list
                .FirstOrDefault(i => i.ChannelId == channelId);

            ChannelSubsCountViewModel channelSubCount = new ChannelSubsCountViewModel
            {
                ChannelId = channel.ChannelId,
                MentorName = channel.MentorName
            };


            foreach (Subscription s in channel.Subscription)
            {
                int currentMonth = int.Parse(s.TimeSubscripted.Month.ToString()) - 1;
                var count = (int)channelSubCount.MonthSubsCount[currentMonth];
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
