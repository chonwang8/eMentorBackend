using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO.QueryAttributesDtos;
using Domain.DTO.ResponseDtos;
using Domain.Services.Interfaces;
using Domain.ViewModels.ChannelModels;
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


        public SharingResponseDto<SharingModel> GetById(string sharingId)
        {
            IEnumerable<SharingModel> result = null;
            SharingResponseDto<SharingModel> responseDto = new SharingResponseDto<SharingModel>
            {
                Status = 0,
                Message = "Success",
                Content = null
            };

            if (sharingId == null)
            {
                responseDto = new SharingResponseDto<SharingModel>
                {
                    Status = 1,
                    Message = "SharingId must be specified",
                    Content = null
                };
                return responseDto;
            };

            try
            {
                result = _uow
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
                    IsDisable = s.IsDisable,
                    ApprovedTime = s.ApprovedTime,
                    Channel = new ChannelViewModel
                    {
                        ChannelId = s.Channel.ChannelId,
                        MentorName = s.Channel.Mentor.User.Email,
                        TopicName = s.Channel.Topic.TopicName
                    }
                });
            }
            catch (Exception e)
            {
                throw e;
            }

            if (result == null)
            {
                responseDto = new SharingResponseDto<SharingModel>
                {
                    Status = 2,
                    Message = "Sharing with id " + sharingId + " does not exist",
                    Content = null
                };
                return responseDto;
            }

            responseDto.Content = result;

            return responseDto;
        }


        public SharingResponseDto Insert(SharingInsertModel sharingInsertModel)
        {
            SharingResponseDto responseDto = null;

            if (sharingInsertModel == null)
            {
                responseDto = new SharingResponseDto
                {
                    Status = 1,
                    Message = "Faulthy sharing info"
                };
                return responseDto;
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
                    IsApproved = false,
                    ImageUrl = sharingInsertModel.imageUrl,
                    ApprovedTime = null
                };

                _uow.GetRepository<Sharing>().Insert(newSharing);
                _uow.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            responseDto = new SharingResponseDto
            {
                Status = 0,
                Message = "Sharing session " + sharingInsertModel.SharingName + " successfully inserted"
            };

            return responseDto;
        }


        public SharingResponseDto Update(SharingModel sharingModel)
        {
            SharingResponseDto responseDto = null;

            if (sharingModel == null)
            {
                responseDto = new SharingResponseDto
                {
                    Status = 1,
                    Message = "Faulthy sharing info"
                };
                return responseDto;
            }

            Sharing existingSharing = null;

            try
            {
                existingSharing = _uow.GetRepository<Sharing>()
                    .GetAll()
                    .FirstOrDefault(s => s.SharingId == sharingModel.SharingId);
            }
            catch (Exception e)
            {
                throw e;
            }

            if (existingSharing == null)
            {
                responseDto = new SharingResponseDto
                {
                    Status = 2,
                    Message = "No existing sharing with specified id found"
                };
                return responseDto;
            }

            DateTime? approveTime = null;

            if (sharingModel.IsApproved == true)
            {
                approveTime = DateTime.Now;
            }
            else if (sharingModel.IsApproved == false)
            {
                approveTime = null;
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
            existingSharing.ImageUrl = sharingModel.imageUrl;
            existingSharing.ApprovedTime = approveTime;

            try
            {
                _uow.GetRepository<Sharing>().Update(existingSharing);
                _uow.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            responseDto = new SharingResponseDto
            {
                Status = 0,
                Message = "Success"
            };

            return responseDto;
        }


        public SharingResponseDto ChangeStatus(string sharingId, bool status)
        {
            SharingResponseDto responseDto = null;
            Guid guid = new Guid(sharingId);

            if (sharingId.Equals(null))
            {
                responseDto = new SharingResponseDto
                {
                    Status = 1,
                    Message = "Faulthy sharing Id."
                };
                return responseDto;
            }

            Sharing existingSharing = _uow
                .GetRepository<Sharing>()
                .GetAll()
                .FirstOrDefault(s => s.SharingId == guid);
            if (existingSharing == null)
            {
                responseDto = new SharingResponseDto
                {
                    Status = 2,
                    Message = "Sharing with specified id not found"
                };
                return responseDto;
            }

            try
            {
                existingSharing.IsDisable = status;
                _uow.GetRepository<Sharing>().Update(existingSharing);
                _uow.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            responseDto = new SharingResponseDto
            {
                Status = 0
            };

            if (status == true)
            {
                responseDto.Message = "Sharing is disabled.";
            }
            else if (status == false)
            {
                responseDto.Message = "Sharing is enabled.";
            }

            return responseDto;
        }


        public SharingResponseDto Delete(string sharingId)
        {
            SharingResponseDto responseDto = null;
            Guid guid = new Guid(sharingId);

            if (sharingId.Equals(null))
            {
                responseDto = new SharingResponseDto
                {
                    Status = 1,
                    Message = "Faulthy sharing Id."
                };
                return responseDto;
            }

            Sharing existingSharing = _uow
                .GetRepository<Sharing>()
                .GetAll()
                .FirstOrDefault(s => s.SharingId == guid);
            if (existingSharing == null)
            {
                responseDto = new SharingResponseDto
                {
                    Status = 2,
                    Message = "Sharing with specified id not found"
                };
                return responseDto;
            }

            try
            {
                _uow.GetRepository<Sharing>().Delete(existingSharing);
                _uow.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            responseDto = new SharingResponseDto
            {
                Status = 0,
                Message = "Successfully remove sharing " + existingSharing.SharingName + " from database."
            };

            return responseDto;
        }

        #endregion



        #region Specialized Methods
        #endregion
    }
}
