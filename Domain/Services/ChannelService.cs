﻿using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO;
using Domain.DTO.QueryAttributesDtos;
using Domain.DTO.ResponseDtos;
using Domain.Services.Interfaces;
using Domain.ViewModels.ChannelModels;
using Domain.ViewModels.SharingModels;
using Domain.ViewModels.SubscriptionModels;
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
        public ChannelResponseDto<ChannelViewModel> GetAll(PagingDto pagingRequest)
        {
            ChannelResponseDto<ChannelViewModel> responseDto =
                new ChannelResponseDto<ChannelViewModel>
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
                .Include(c => c.Mentor.User)
                .Include(c => c.Topic)
                .Select(s => new ChannelViewModel
                {
                    ChannelId = s.ChannelId,
                    MentorName = s.Mentor.User.Email,
                    TopicName = s.Topic.TopicName
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

            if (pagingRequest.PageIndex != null && pagingRequest.PageSize != null)
            {
                result = result.Skip((pagingRequest.PageIndex.GetValueOrDefault() - 1) * pagingRequest.PageSize.GetValueOrDefault()).Take(pagingRequest.PageSize.GetValueOrDefault());
            }

            //  finalize
            responseDto.Content = result;

            return responseDto;
        }


        public ChannelResponseDto<ChannelModel> GetById(string channelId)
        {
            IEnumerable<ChannelModel> result = null;
            ChannelResponseDto<ChannelModel> responseDto = new ChannelResponseDto<ChannelModel>
            {
                Status = 0,
                Message = "Success",
                Content = null
            };

            if (channelId == null)
            {
                responseDto = new ChannelResponseDto<ChannelModel>
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
                .Include(c => c.Mentor.User)
                .Include(c => c.Sharing.Select(s => new SharingViewModel
                {
                    SharingId = s.SharingId,
                    SharingName = s.SharingName,
                    Price = s.Price,
                    MentorName = s.Channel.Mentor.User.Fullname,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    ImageUrl = s.ImageUrl,
                    IsApproved = s.IsApproved
                }))
                .Include(c => c.Subscription.Select(s => new SubscriptionViewModel
                {
                    SubscriptionId = s.SubscriptionId,
                    ChannelId = s.ChannelId,
                    MenteeId = s.MenteeId,
                    TimeSubscripted = s.TimeSubscripted,
                    IsDisable = s.IsDisable
                }))

                .Where(c => c.ChannelId.Equals(new Guid(channelId)))
                .Select(c => new ChannelModel
                {
                    ChannelId = c.ChannelId,
                    Mentor = null,
                    Topic = null,
                    Sharing = null,
                    Subscription = null
                });
            }
            catch (Exception e)
            {
                throw e;
            }

            if (result == null)
            {
                responseDto = new ChannelResponseDto<ChannelModel>
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


        public ChannelResponseDto Insert(ChannelInsertModel channelInsertModel)
        {
            ChannelResponseDto responseDto = null;

            if (channelInsertModel == null)
            {
                responseDto = new ChannelResponseDto
                {
                    Status = 1,
                    Message = "Faulthy sharing info"
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

            responseDto = new ChannelResponseDto
            {
                Status = 0,
                Message = "Channel successfully inserted"
            };

            return responseDto;
        }


        public ChannelResponseDto Update(ChannelUpdateModel channelUpdateModel)
        {
            ChannelResponseDto responseDto = null;

            if (channelUpdateModel == null)
            {
                responseDto = new ChannelResponseDto
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
                responseDto = new ChannelResponseDto
                {
                    Status = 2,
                    Message = "No existing channel with specified id found"
                };
                return responseDto;
            }

            existingChannel.ChannelId = channelUpdateModel.ChannelId;
            existingChannel.MentorId = channelUpdateModel.MentorId;
            existingChannel.TopicId = channelUpdateModel.TopicId;

            try
            {
                _uow.GetRepository<Channel>().Update(existingChannel);
                _uow.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            responseDto = new ChannelResponseDto
            {
                Status = 0,
                Message = "Success"
            };

            return responseDto;
        }


        public ChannelResponseDto ChangeStatus(string channelId, bool status)
        {
            ChannelResponseDto responseDto = null;
            Guid guid = new Guid(channelId);

            if (channelId.Equals(null))
            {
                responseDto = new ChannelResponseDto
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
                responseDto = new ChannelResponseDto
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

            responseDto = new ChannelResponseDto
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


        public ChannelResponseDto Delete(string channelId)
        {
            ChannelResponseDto responseDto = null;
            Guid guid = new Guid(channelId);

            if (channelId.Equals(null))
            {
                responseDto = new ChannelResponseDto
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
                responseDto = new ChannelResponseDto
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

            responseDto = new ChannelResponseDto
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
        #endregion
    }
}
