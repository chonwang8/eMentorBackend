using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.Models.SubscriptionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Models.MenteeModels;
using Domain.Models.ChannelModels;
using Domain.Models.EnrollModels;
using Domain.DTO.ResponseDtos;

namespace Domain.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        #region Classes and Constructor
        protected readonly IUnitOfWork _uow;

        public SubscriptionService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        #endregion Classes and Constructor


        #region RESTful API methods

        public IEnumerable<SubscriptionViewModel> GetAll()
        {
            IEnumerable<SubscriptionViewModel> result = _uow
                .GetRepository<Subscription>()
                .GetAll()

                .Include(s => s.Mentee)
                .ThenInclude(s => s.User)
                .Include(s => s.Channel)
                .ThenInclude(s => s.Mentor)
                .ThenInclude(s => s.User)
                .Include(s => s.Channel)
                .ThenInclude(s => s.Topic)

                .Select(s => new SubscriptionViewModel
                {
                    SubscriptionId = s.SubscriptionId,
                    MenteeName = s.Mentee.User.Email,
                    ChannelMentor = s.Channel.Mentor.User.Email,
                    ChannelTopic = s.Channel.Topic.TopicName,
                    TimeSubscripted = s.TimeSubscripted,
                    IsDisable = s.IsDisable
                });
            
            return result;
        }

        public IEnumerable<SubscriptionModel> GetById(string subscriptionId)
        {
            if (subscriptionId == null)
            {
                return null;
            }

            IEnumerable<SubscriptionModel> result = _uow
                .GetRepository<Subscription>()
                .GetAll()

                .Include(s => s.Mentee)
                .ThenInclude(s => s.User)
                .Include(s => s.Enroll)
                .ThenInclude(s => s.Sharing)
                .Include(s => s.Channel)
                .ThenInclude(s => s.Topic)
                
                .Where(s => s.SubscriptionId.Equals(new Guid(subscriptionId)))
                .Select(s => new SubscriptionModel
                {
                    SubscriptionId = s.SubscriptionId,
                    TimeSubscripted = s.TimeSubscripted,
                    IsDisable = s.IsDisable,
                    Mentee = new MenteeViewModel
                    {
                        MenteeId = s.Mentee.MenteeId,
                        Email = s.Mentee.User.Email,
                        Fullname = s.Mentee.User.Fullname,
                        Description = s.Mentee.User.Description,
                        AvatarUrl = s.Mentee.User.AvatarUrl
                    },
                    Channel = new ChannelViewModel
                    {
                        ChannelId = s.ChannelId,
                        MentorName = s.Channel.Mentor.User.Email,
                        TopicName = s.Channel.Topic.TopicName
                    },
                    Enroll = s.Enroll.Select(e => new EnrollViewModel 
                    {
                        EnrollId = e.EnrollId,
                        SharingId = e.SharingId,
                        SharingName = e.Sharing.SharingName,
                        IsDisable = e.IsDisable
                    }).ToList()
                });

            return result;
        }


        public int Insert(SubscriptionInsertModel subscriptionInsertModel)
        {
            int result = 0;

            if (subscriptionInsertModel == null)
            {
                result = 0;
                return result;
            }

            Subscription subscriptionInsert = new Subscription
            {
                SubscriptionId = Guid.NewGuid(),
                ChannelId = subscriptionInsertModel.ChannelId,
                MenteeId = subscriptionInsertModel.MenteeId,
                IsDisable = subscriptionInsertModel.IsDisable,
                TimeSubscripted = DateTime.Now
            };

            try
            {
                _uow.GetRepository<Subscription>().Insert(subscriptionInsert);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public int Update(SubscriptionUpdateModel subscriptionUpdateModel)
        {
            int result = 0;

            if (subscriptionUpdateModel == null)
            {
                result = 0;
                return result;
            }


            Subscription existingSubscription = _uow
                .GetRepository<Subscription>()
                .GetAll()
                .SingleOrDefault(s => s.SubscriptionId == subscriptionUpdateModel.SubscriptionId);

            if (existingSubscription == null)
            {
                result = 1;
                return result;
            }

            existingSubscription.MenteeId = subscriptionUpdateModel.MenteeId;
            existingSubscription.ChannelId = subscriptionUpdateModel.ChannelId;
            existingSubscription.IsDisable = subscriptionUpdateModel.IsDisable;

            try
            {
                _uow.GetRepository<Subscription>().Update(existingSubscription);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public int ChangeStatus(string subscriptionId, bool status)
        {
            int result = 0;
            Guid guid = new Guid(subscriptionId);

            if (subscriptionId.Equals(null))
            {
                result = 0;
                return result;
            }

            Subscription existingSubscription = _uow
                .GetRepository<Subscription>()
                .GetAll()
                .FirstOrDefault(m => m.SubscriptionId == guid);
            if (existingSubscription == null)
            {
                result = 1;
                return result;
            }

            existingSubscription.IsDisable = status;

            try
            {
                _uow.GetRepository<Subscription>().Update(existingSubscription);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public int Delete(string subscriptionId)
        {
            int result = 0;

            if (subscriptionId.Equals(null))
            {
                result = 0;
                return result;
            }

            Guid guid = new Guid(subscriptionId);

            Subscription existingSubscription = _uow
                .GetRepository<Subscription>()
                .GetAll()
                .FirstOrDefault(t => t.SubscriptionId == guid);
            if (existingSubscription == null)
            {
                result = 1;
                return result;
            }

            try
            {
                _uow.GetRepository<Subscription>().Delete(existingSubscription);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }


            return result;
        }

        public BaseResponseDto Delete(string menteeId, string channelId)
        {
            BaseResponseDto responseDto = null;
            Guid menteeGuid = new Guid(menteeId);
            Guid channelGuid = new Guid(menteeId);

            #region Check input
            if (menteeId.Equals(null))
            {
                responseDto = new BaseResponseDto
                {
                    Status = 1,
                    Message = "Faulthy mentee Id."
                };
                return responseDto;
            }
            if (channelId.Equals(null))
            {
                responseDto = new BaseResponseDto
                {
                    Status = 1,
                    Message = "Faulthy channel Id."
                };
                return responseDto;
            }
            #endregion

            Subscription existingSubscription = _uow
                .GetRepository<Subscription>()
                .GetAll()
                .FirstOrDefault(s => s.ChannelId == channelGuid && s.MenteeId == menteeGuid);

            if (existingSubscription == null)
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
                _uow.GetRepository<Subscription>().Delete(existingSubscription);
                _uow.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            responseDto = new BaseResponseDto
            {
                Status = 0,
                Message = "Unsubscribed."
            };

            return responseDto;
        }
        #endregion
    }
}
