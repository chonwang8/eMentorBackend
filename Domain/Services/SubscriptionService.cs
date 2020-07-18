using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.Models.SubscriptionModels;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<SubscriptionModel> GetAll()
        {
            IEnumerable<SubscriptionModel> result = _uow
                .GetRepository<Subscription>()
                .GetAll()
                .Select(s => new SubscriptionModel
                {
                    SubcriptionId = s.SubscriptionId,
                    ChannelId = s.ChannelId,
                    MenteeId = s.MenteeId,
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
                .Where(s => s.SubscriptionId.Equals(new Guid(subscriptionId)))
                .Select(s => new SubscriptionModel
                {
                    SubcriptionId = s.SubscriptionId,
                    ChannelId = s.ChannelId,
                    MenteeId = s.MenteeId,
                    IsDisable = s.IsDisable
                });

            return result;
        }


        public int Insert(SubscriptionModel subscriptionViewModel)
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
                .SingleOrDefault(s => s.SubscriptionId == subscriptionViewModel.SubcriptionId);
            if (existingSubscription != null)
            {
                result = 1;
                return result;
            }


            Subscription subscriptionInsert = new Subscription
            {
                SubscriptionId = subscriptionViewModel.SubcriptionId,
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

        public int Update(SubscriptionModel subscriptionViewModel)
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
                .SingleOrDefault(s => s.SubscriptionId == subscriptionViewModel.SubcriptionId);

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

        #endregion
    }
}
