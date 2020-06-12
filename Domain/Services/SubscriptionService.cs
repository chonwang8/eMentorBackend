using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Services
{
    public class SubscriptionService : ISubcriptionService
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
                .Select(s => new SubscriptionViewModel
                {
                    SubcriptionId = s.SubcriptionId,
                    ChannelId = s.ChannelId,
                    MenteeId = s.MenteeId,
                    IsDisable = s.IsDisable
                });

            return result;
        }

        public IEnumerable<SubscriptionViewModel> GetById(string subscriptionId)
        {
            if (subscriptionId == null)
            {
                return null;
            }

            IEnumerable<SubscriptionViewModel> result = _uow
                .GetRepository<Subscription>()
                .GetAll()
                .Where(s => s.SubcriptionId.Equals(new Guid(subscriptionId)))
                .Select(s => new SubscriptionViewModel
                {
                    SubcriptionId = s.SubcriptionId,
                    ChannelId = s.ChannelId,
                    MenteeId = s.MenteeId,
                    IsDisable = s.IsDisable
                });

            return result;
        }


        public int Insert(SubscriptionViewModel subscriptionViewModel)
        {
            int result = 0;

            if (subscriptionViewModel == null)
            {
                result = 0;
                return result;
            }


            Subscription existingSubscription = _uow
                .GetRepository<Subscription>()
                .GetAll()
                .SingleOrDefault(s => s.SubcriptionId == subscriptionViewModel.SubcriptionId);
            if (existingSubscription != null)
            {
                result = 1;
                return result;
            }


            Subscription subscriptionInsert = new Subscription
            {
                SubcriptionId = subscriptionViewModel.SubcriptionId,
                ChannelId = subscriptionViewModel.ChannelId,
                MenteeId = subscriptionViewModel.MenteeId,
                IsDisable = subscriptionViewModel.IsDisable
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

        public int Update(SubscriptionViewModel subscriptionViewModel)
        {
            int result = 0;

            if (subscriptionViewModel == null)
            {
                result = 0;
                return result;
            }


            Subscription existingSubscription = _uow
                .GetRepository<Subscription>()
                .GetAll()
                .SingleOrDefault(s => s.SubcriptionId == subscriptionViewModel.SubcriptionId);
            
            if (existingSubscription == null)
            {
                result = 1;
                return result;
            }

            existingSubscription.MenteeId = subscriptionViewModel.MenteeId;
            existingSubscription.IsDisable = subscriptionViewModel.IsDisable;
            existingSubscription.ChannelId = subscriptionViewModel.ChannelId;


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
        public int Delete(string topicId)
        {
            int result = 0;
            Guid guid = new Guid(topicId);

            if (topicId.Equals(null))
            {
                result = 0;
                return result;
            }

            Topic existingTopic = _uow
                .GetRepository<Topic>()
                .GetAll()
                .FirstOrDefault(t => t.TopicId == guid);
            if (existingTopic == null)
            {
                result = 1;
                return result;
            }

            try
            {
                _uow.GetRepository<Topic>().Delete(existingTopic);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }


            return result;
        }

        #endregion
    }
}
