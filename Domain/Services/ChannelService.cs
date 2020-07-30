using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO.ResponseDtos;
using Domain.Models.ChannelModels;
using Domain.Models.MentorModels;
using Domain.Models.RatingModels;
using Domain.Models.SharingModels;
using Domain.Models.SubscriptionModels;
using Domain.Models.TopicModels;
using Domain.Services.Interfaces;
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



        #region CRUD Methods
        public BaseResponseDto<ChannelViewModel> GetAll()
        {
            BaseResponseDto<ChannelViewModel> responseDto =
                new BaseResponseDto<ChannelViewModel>
                {
                    Status = 0,
                    Message = "Success",
                    Content = null
                };

            IEnumerable<ChannelViewModel> result = null;

            try
            {
                result = _uow
                .GetRepository<Channel>()
                .GetAll()
                .Include(c => c.Mentor)
                .ThenInclude(c => c.User)
                .Include(c => c.Topic)
                .Select(s => new ChannelViewModel
                {
                    ChannelId = s.ChannelId,
                    MentorName = s.Mentor.User.Email,
                    TopicName = s.Topic.TopicName,
                    IsDisable = s.IsDisable
                });
            }
            catch (Exception e)
            {
                throw e;
            }

            if (result == null)
            {
                responseDto.Status = 1;
                responseDto.Message = "There are no channels in the system";
            };

            //  finalize
            responseDto.Content = result;
            return responseDto;
        }


        public BaseResponseDto<ChannelModel> GetById(string channelId)
        {
            IEnumerable<ChannelModel> result = null;
            BaseResponseDto<ChannelModel> responseDto = new BaseResponseDto<ChannelModel>
            {
                Status = 0,
                Message = "Success",
                Content = null
            };

            if (channelId == null)
            {
                responseDto = new BaseResponseDto<ChannelModel>
                {
                    Status = 1,
                    Message = "ChannelId must be specified",
                    Content = null
                };
                return responseDto;
            };

            try
            {
                result = _uow
                .GetRepository<Channel>()
                .GetAll()

                .Include(c => c.Topic)
                .Include(c => c.Mentor)
                .ThenInclude(c => c.User)
                .Include(c => c.Mentor)
                .ThenInclude(c => c.Rating)
                .Include(c => c.Subscription)
                .ThenInclude(c => c.Mentee)
                .ThenInclude(c => c.User)
                .Include(c => c.Sharing)
                .Include(c => c.Subscription)

                .Where(c => c.ChannelId.Equals(new Guid(channelId)))
                .Select(c => new ChannelModel
                {
                    ChannelId = c.ChannelId,
                    IsDisable = c.IsDisable,
                    Mentor = new MentorViewModel
                    {
                        MentorId = c.Mentor.MentorId,
                        Email = c.Mentor.User.Email,
                        Fullname = c.Mentor.User.Fullname,
                        AvatarUrl = c.Mentor.User.AvatarUrl,
                        Description = c.Mentor.User.Description,
                        Rating = new RatingViewModel
                        {
                            RatingScore = c.Mentor.Rating.RatingScore
                        },
                        Channels = new List<ChannelViewModel>() //  its supposed to be empty
                    },
                    Topic = new TopicViewModel
                    {
                        TopicId = c.Topic.TopicId,
                        TopicName = c.Topic.TopicName,
                        MajorId = c.Topic.MajorId,
                        CreatedBy = c.Topic.CreatedBy
                    },
                    Sharing = c.Sharing.Select(s => new SharingViewModel
                    {
                        SharingId = s.SharingId,
                        SharingName = s.SharingName,
                        MentorName = s.Channel.Mentor.User.Email,
                        StartTime = s.StartTime,
                        EndTime = s.EndTime,
                        ImageUrl = s.ImageUrl,
                        Price = s.Price,
                        IsApproved = s.IsApproved
                    }).ToList(),
                    Subscription = c.Subscription.Select(s => new SubscriptionViewModel
                    {
                        SubscriptionId = s.SubscriptionId,
                        MenteeName = s.Mentee.User.Email,
                        ChannelMentor = s.Channel.Mentor.User.Email,
                        ChannelTopic = s.Channel.Topic.TopicName,
                        TimeSubscripted = s.TimeSubscripted,
                        IsDisable = s.IsDisable
                    }).ToList()
                });
            }
            catch (Exception e)
            {
                throw e;
            }

            if (result == null)
            {
                responseDto = new BaseResponseDto<ChannelModel>
                {
                    Status = 2,
                    Message = "Channel with id " + channelId + " does not exist",
                    Content = null
                };
                return responseDto;
            }

            responseDto.Content = result;

            return responseDto;
        }


        public BaseResponseDto Insert(ChannelInsertModel channelInsertModel)
        {
            BaseResponseDto responseDto = null;

            if (channelInsertModel == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 1,
                    Message = "Faulthy channel info"
                };
                return responseDto;
            }

            Channel existingChannel = null;

            try
            {
                existingChannel = _uow
                    .GetRepository<Channel>()
                    .GetAll()
                    .FirstOrDefault(c => 
                    c.TopicId == channelInsertModel.TopicId && 
                    c.MentorId == channelInsertModel.MentorId);
            }
            catch (Exception e)
            {
                throw e;
            }

            if (existingChannel != null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 2,
                    Message = "Channel already exist"
                };
                return responseDto;
            }

            try
            {
                Channel newChannel = new Channel
                {
                    ChannelId = Guid.NewGuid(),
                    TopicId = channelInsertModel.TopicId,
                    MentorId = channelInsertModel.MentorId,
                    IsDisable = false
                };

                _uow.GetRepository<Channel>().Insert(newChannel);
                _uow.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            responseDto = new BaseResponseDto
            {
                Status = 0,
                Message = "Channel successfully inserted"
            };

            return responseDto;
        }


        public BaseResponseDto Update(ChannelUpdateModel channelUpdateModel)
        {
            BaseResponseDto responseDto = null;

            if (channelUpdateModel == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 1,
                    Message = "Faulthy channel info"
                };
                return responseDto;
            }

            Channel existingChannel = null;

            try
            {
                existingChannel = _uow.GetRepository<Channel>()
                    .GetAll()
                    .FirstOrDefault(s => s.ChannelId == channelUpdateModel.ChannelId);
            }
            catch (Exception e)
            {
                throw e;
            }

            if (existingChannel == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 2,
                    Message = "No existing channel with specified id found"
                };
                return responseDto;
            }

            existingChannel.MentorId = channelUpdateModel.MentorId;
            existingChannel.TopicId = channelUpdateModel.TopicId;
            existingChannel.IsDisable = channelUpdateModel.IsDisable;

            try
            {
                _uow.GetRepository<Channel>().Update(existingChannel);
                _uow.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            responseDto = new BaseResponseDto
            {
                Status = 0,
                Message = "Success"
            };

            return responseDto;
        }


        public BaseResponseDto ChangeStatus(string channelId, bool status)
        {
            BaseResponseDto responseDto = null;
            Guid guid = new Guid(channelId);

            if (channelId.Equals(null))
            {
                responseDto = new BaseResponseDto
                {
                    Status = 1,
                    Message = "Faulthy channel Id."
                };
                return responseDto;
            }

            Channel existingChannel = _uow
                .GetRepository<Channel>()
                .GetAll()
                .FirstOrDefault(s => s.ChannelId == guid);
            if (existingChannel == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 2,
                    Message = "Channel with specified id not found"
                };
                return responseDto;
            }

            try
            {
                existingChannel.IsDisable = status;
                _uow.GetRepository<Channel>().Update(existingChannel);
                _uow.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            responseDto = new BaseResponseDto
            {
                Status = 0
            };

            if (status == true)
            {
                responseDto.Message = "Channel is disabled.";
            }
            else if (status == false)
            {
                responseDto.Message = "Channel is enabled.";
            }

            return responseDto;
        }


        public BaseResponseDto Delete(string channelId)
        {
            BaseResponseDto responseDto = null;
            Guid guid = new Guid(channelId);

            if (channelId.Equals(null))
            {
                responseDto = new BaseResponseDto
                {
                    Status = 1,
                    Message = "Faulthy channel Id."
                };
                return responseDto;
            }

            Channel existingChannel = _uow
                .GetRepository<Channel>()
                .GetAll()
                .FirstOrDefault(s => s.ChannelId == guid);
            if (existingChannel == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 2,
                    Message = "Channel with specified id not found"
                };
                return responseDto;
            }

            try
            {
                _uow.GetRepository<Channel>().Delete(existingChannel);
                _uow.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            responseDto = new BaseResponseDto
            {
                Status = 0,
                Message = "Successfully remove channel " + existingChannel.ChannelId + " from database."
            };

            return responseDto;
        }


        #endregion



        #region Specialized Methods
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

        public ChannelSubsCountViewModel GetChannelSubCount(string channelId)
        {
            BaseResponseDto<ChannelModel> query = null;
            ChannelModel channel = null;

            try
            {
                query = GetById(channelId);
                channel = query.Content.First();
            }
            catch (Exception e)
            {
                throw e;
            }

            ChannelSubsCountViewModel channelSubCount = new ChannelSubsCountViewModel
            {
                ChannelId = channel.ChannelId,
                MentorName = channel.Mentor.Email
            };


            foreach (SubscriptionViewModel s in channel.Subscription)
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
        #endregion
    }
}
