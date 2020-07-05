using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.ViewModels.SharingModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class SharingService : ISharingService
    {
        #region Classes and Constructor
        protected readonly IUnitOfWork _uow;

        public SharingService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        #endregion Classes and Constructor



        #region CRUD Methods

        public IEnumerable<SharingViewModel> GetAll(GetAllDTO request)
        {
            IEnumerable<SharingViewModel> result = _uow
                .GetRepository<Sharing>()
                .GetAll()
                .Include(s => s.Channel.Topic)
                .Select(s => new SharingViewModel
                {
                    SharingId = s.SharingId,
                    SharingName = s.SharingName,
                    Price = s.Price,
                    MentorName = s.Channel.Mentor.User.Fullname,
                    ImageUrl = s.ImageUrl,
                    IsApproved = s.IsApproved
                });
            result = result.Where(s => s.IsApproved == request.IsApproved);
            result = result.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            return result;
        }


        public IEnumerable<SharingModel> GetById(string sharingId)
        {
            if (sharingId == null)
            {
                return null;
            }

            IEnumerable<SharingModel> result = _uow
                .GetRepository<Sharing>()
                .GetAll()
                .Where(s => s.SharingId.Equals(new Guid(sharingId)))
                .Select(s => new SharingModel
                {
                    SharingId = s.SharingId,
                    SharingName = s.SharingName,
                    Description = s.Description,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    Maximum = s.Maximum,
                    Price = s.Price,
                    ChannelId = s.ChannelId,
                    imageUrl = s.ImageUrl,
                    TopicName = s.Channel.Topic.TopicName,
                    IsApproved = s.IsApproved,
                    IsDisable = s.IsDisable
                });

            return result;
        }


        public int Insert(SharingModel sharingViewModel)
        {
            int result = 0;

            if (sharingViewModel == null)
            {
                result = 0;
                return result;
            }


            Sharing sharingInDb = _uow
                .GetRepository<Sharing>()
                .GetAll()
                .SingleOrDefault(s => s.SharingId == sharingViewModel.SharingId);
            if (sharingInDb != null)
            {
                result = 1;
                return result;
            }


            Sharing newSharing = new Sharing
            {
                SharingId = sharingViewModel.SharingId,
                SharingName = sharingViewModel.SharingName,
                Description = sharingViewModel.Description,
                StartTime = sharingViewModel.StartTime,
                EndTime = sharingViewModel.EndTime,
                Maximum = sharingViewModel.Maximum,
                Price = sharingViewModel.Price,
                ChannelId = sharingViewModel.ChannelId,
                IsDisable = false
            };


            try
            {
                _uow.GetRepository<Sharing>().Insert(newSharing);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }


        public int Update(SharingModel sharingViewModel)
        {
            int result = 0;

            if (sharingViewModel == null)
            {
                result = 0;
                return result;
            }

            Sharing existingSharing = _uow.GetRepository<Sharing>()
                .GetAll()
                .FirstOrDefault(s => s.SharingId == sharingViewModel.SharingId);

            if (existingSharing == null)
            {
                result = 1;
                return result;
            }

            existingSharing.SharingName = sharingViewModel.SharingName;
            existingSharing.Description = sharingViewModel.Description;
            existingSharing.StartTime = sharingViewModel.StartTime;
            existingSharing.EndTime = sharingViewModel.EndTime;
            existingSharing.Maximum = sharingViewModel.Maximum;
            existingSharing.Price = sharingViewModel.Price;
            existingSharing.ChannelId = sharingViewModel.ChannelId;

            try
            {
                _uow.GetRepository<Sharing>().Update(existingSharing);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }


        public int ChangeStatus(string sharingId, bool status)
        {
            int result = 0;
            Guid guid = new Guid(sharingId);

            if (sharingId.Equals(null))
            {
                result = 0;
                return result;
            }

            Sharing existingSharing = _uow
                .GetRepository<Sharing>()
                .GetAll()
                .FirstOrDefault(s => s.SharingId == guid);
            if (existingSharing == null)
            {
                result = 1;
                return result;
            }

            //  existingSharing.IsDisable = status;

            try
            {
                _uow.GetRepository<Sharing>().Update(existingSharing);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }


        public int Delete(string sharingId)
        {
            int result = 0;
            Guid sharingGuid = new Guid(sharingId);

            if (sharingId.Equals(null))
            {
                result = 0;
                return result;
            }

            Sharing existingSharing = _uow
                .GetRepository<Sharing>()
                .GetAll()
                .FirstOrDefault(s => s.SharingId == sharingGuid);
            if (existingSharing == null)
            {
                result = 1;
                return result;
            }

            try
            {
                _uow.GetRepository<Sharing>().Delete(existingSharing);
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



        #region Specialized Methods
        public IEnumerable<SharingModel> GetAvailableSharings()
        {
            IEnumerable<SharingModel> result = _uow
                .GetRepository<Sharing>()
                .GetAll()
                .Include(s => s.Channel.Topic)
                .Select(s => new SharingModel
                {
                    SharingId = s.SharingId,
                    SharingName = s.SharingName,
                    Description = s.Description,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    Maximum = s.Maximum,
                    Price = s.Price,
                    ChannelId = s.ChannelId,
                    imageUrl = s.ImageUrl,
                    TopicName = s.Channel.Topic.TopicName
                });
            //result = result.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            
            return result;
        }


        #endregion
    }
}
