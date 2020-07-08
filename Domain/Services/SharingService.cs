using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO.QueryAttributesDtos;
using Domain.DTO.ResponseDtos;
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
        public SharingResponseDto<SharingViewModel> GetAll(PagingDto pagingRequest, FilterDto filterRequest)
        {
            SharingResponseDto<SharingViewModel> responseDto = new SharingResponseDto<SharingViewModel>
            {
                Status = 0,
                Message = "Success",
                Content = null
            };
            IEnumerable<SharingViewModel> result = null;

            try
            {
                result = _uow
                .GetRepository<Sharing>()
                .GetAll()
                .Include(s => s.Channel.Topic)
                .Select(s => new SharingViewModel
                {
                    SharingId = s.SharingId,
                    SharingName = s.SharingName,
                    Price = s.Price,
                    MentorName = s.Channel.Mentor.User.Fullname,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    ImageUrl = s.ImageUrl,
                    IsApproved = s.IsApproved
                })
                .OrderBy(s => s.StartTime);
            }
            catch (Exception e)
            {
                throw e;
            }

            if (result == null)
            {
                responseDto.Status = 1;
                responseDto.Message = "There are no sharings in the system";
            };

            if (filterRequest.IsApproved != null)
            {
                result = result.Where(s => s.IsApproved == filterRequest.IsApproved);
                if (result == null || result.Count() <= 0)
                {
                    responseDto = new SharingResponseDto<SharingViewModel>
                    {
                        Status = 2,
                        Message = "There are no Sharing with isApproved(" + filterRequest.IsApproved + ") status"
                    };
                }
            }

            if (pagingRequest.PageIndex != null && pagingRequest.PageSize != null)
            {
                result = result.Skip((pagingRequest.PageIndex.GetValueOrDefault() - 1) * pagingRequest.PageSize.GetValueOrDefault()).Take(pagingRequest.PageSize.GetValueOrDefault());
            }

            //  finalize
            responseDto.Content = result;

            return responseDto;
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
                    IsApproved = s.IsApproved,
                    IsDisable = s.IsDisable
                });

            return result;
        }


        public SharingResponseDto Insert(SharingInsertModel sharingInsertModel)
        {
            if (sharingInsertModel == null)
            {
                return new SharingResponseDto
                {
                    Status = 1,
                    Message = "Faulthy sharing info"
                };
            }

            try
            {
                Sharing newSharing = new Sharing
                {
                    SharingId = Guid.NewGuid(),
                    SharingName = sharingInsertModel.SharingName,
                    Description = sharingInsertModel.Description,
                    StartTime = sharingInsertModel.StartTime,
                    EndTime = sharingInsertModel.EndTime,
                    Maximum = sharingInsertModel.Maximum,
                    Price = sharingInsertModel.Price,
                    ChannelId = sharingInsertModel.ChannelId,
                    IsDisable = false,
                    IsApproved = false
                };

                _uow.GetRepository<Sharing>().Insert(newSharing);
                _uow.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            return new SharingResponseDto
            {
                Status = 0,
                Message = "Sharing session " + sharingInsertModel.SharingName + " successfully inserted"
            };
        }


        public int Update(SharingModel sharingModel)
        {
            int result = 0;

            if (sharingModel == null)
            {
                result = 0;
                return result;
            }

            Sharing existingSharing = _uow.GetRepository<Sharing>()
                .GetAll()
                .FirstOrDefault(s => s.SharingId == sharingModel.SharingId);

            if (existingSharing == null)
            {
                result = 1;
                return result;
            }

            existingSharing.SharingName = sharingModel.SharingName;
            existingSharing.Description = sharingModel.Description;
            existingSharing.StartTime = sharingModel.StartTime;
            existingSharing.EndTime = sharingModel.EndTime;
            existingSharing.Maximum = sharingModel.Maximum;
            existingSharing.Price = sharingModel.Price;
            existingSharing.ChannelId = sharingModel.ChannelId;
            existingSharing.IsDisable = sharingModel.IsDisable;
            existingSharing.IsApproved = sharingModel.IsApproved;

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
                });
            //result = result.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);

            return result;
        }


        #endregion
    }
}
