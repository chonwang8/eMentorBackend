using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO.QueryAttributesDtos;
using Domain.DTO.ResponseDtos;
using Domain.Services.Interfaces;
using Domain.Models.ChannelModels;
using Domain.Models.SharingModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models.EnrollModels;

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
        public BaseResponseDto<SharingViewModel> GetAll(FilterDto filterRequest)
        {
            BaseResponseDto<SharingViewModel> responseDto = new BaseResponseDto<SharingViewModel>
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
                    Location = s.Location,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    ImageUrl = s.ImageUrl,
                    IsApproved = s.IsApproved,
                    IsDisable = s.IsDisable
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
                    responseDto = new BaseResponseDto<SharingViewModel>
                    {
                        Status = 2,
                        Message = "There are no Sharing with isApproved(" + filterRequest.IsApproved + ") status"
                    };
                    return responseDto;
                }
            }

            //  finalize
            responseDto.Content = result;

            return responseDto;
        }


        public BaseResponseDto<SharingViewModel> GetByName(string sharingName, FilterDto filterRequest)
        {
            IEnumerable<SharingViewModel> result = null;
            BaseResponseDto<SharingViewModel> responseDto = new BaseResponseDto<SharingViewModel>
            {
                Status = 0,
                Message = "Success",
                Content = null
            };

            if (sharingName == null)
            {
                responseDto = new BaseResponseDto<SharingViewModel>
                {
                    Status = 1,
                    Message = "Sharing name not specified",
                    Content = null
                };
                return responseDto;
            };

            try
            {
                result = _uow
                .GetRepository<Sharing>()
                .GetAll()
                .Include(s => s.Channel)
                .ThenInclude(s => s.Topic)
                .Where(s => s.IsApproved == true && s.IsDisable == false)
                .Select(s => new SharingViewModel
                {
                    SharingId = s.SharingId,
                    SharingName = s.SharingName,
                    Price = s.Price,
                    MentorName = s.Channel.Mentor.User.Fullname,
                    Location = s.Location,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    ImageUrl = s.ImageUrl,
                    IsApproved = s.IsApproved,
                    IsDisable = s.IsDisable
                });
            }
            catch (Exception e)
            {
                throw e;
            }

            if (result == null)
            {
                responseDto = new BaseResponseDto<SharingViewModel>
                {
                    Status = 2,
                    Message = "Result not found for " + sharingName + " sharing name",
                    Content = null
                };
                return responseDto;
            }

            if (filterRequest.IsFuture != null)
            {
                if (filterRequest.IsFuture == true)
                {
                    result = result.Where(s => s.StartTime.Value.CompareTo(DateTime.Now) > 0);
                }
                else if (filterRequest.IsFuture == false)
                {
                    result = result.Where(s => s.StartTime.Value.CompareTo(DateTime.Now) < 0);
                }
                else
                {
                    throw new Exception("Internal server error at comparing time");
                }


                if (result == null || result.Count() <= 0)
                {
                    responseDto = new BaseResponseDto<SharingViewModel>
                    {
                        Status = 2,
                        Message = "There are no channel with specified timestamp requirement"
                    };
                    return responseDto;
                }
            }

            //  finalize
            responseDto.Content = result;
            return responseDto;
        }


        public BaseResponseDto<SharingModel> GetById(string sharingId)
        {
            IEnumerable<SharingModel> result = null;
            BaseResponseDto<SharingModel> responseDto = new BaseResponseDto<SharingModel>
            {
                Status = 0,
                Message = "Success",
                Content = null
            };

            if (sharingId == null)
            {
                responseDto = new BaseResponseDto<SharingModel>
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

                .Include(s => s.Channel)
                .ThenInclude(s => s.Mentor)
                .ThenInclude(s => s.User)
                .Include(s => s.Enroll)
                .ThenInclude(s => s.Subscription)
                .ThenInclude(s => s.Mentee)
                .ThenInclude(s => s.User)

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
                        TopicName = s.Channel.Topic.TopicName,
                        IsDisable = s.IsDisable
                    },
                    Enroll = s.Enroll.Select(e => new EnrollViewModel
                    {
                        EnrollId = e.EnrollId,
                        SharingId = e.Sharing.SharingId,
                        SharingName = e.Sharing.SharingName,
                        MenteeName = e.Subscription.Mentee.User.Email,
                        MentorName = e.Sharing.Channel.Mentor.User.Email,
                        SubscriptionId = e.SubscriptionId,
                        IsDisable = e.IsDisable
                    }).ToList()
                }); ;
            }
            catch (Exception e)
            {
                throw e;
            }

            if (result == null)
            {
                responseDto = new BaseResponseDto<SharingModel>
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


        public BaseResponseDto Insert(SharingInsertModel sharingInsertModel)
        {
            BaseResponseDto responseDto = null;

            if (sharingInsertModel == null)
            {
                responseDto = new BaseResponseDto
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

            responseDto = new BaseResponseDto
            {
                Status = 0,
                Message = "Sharing session " + sharingInsertModel.SharingName + " successfully inserted"
            };

            return responseDto;
        }


        public BaseResponseDto Update(SharingModel sharingModel)
        {
            BaseResponseDto responseDto = null;

            if (sharingModel == null)
            {
                responseDto = new BaseResponseDto
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
                responseDto = new BaseResponseDto
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

            responseDto = new BaseResponseDto
            {
                Status = 0,
                Message = "Success"
            };

            return responseDto;
        }


        public BaseResponseDto ChangeStatus(string sharingId, bool status)
        {
            BaseResponseDto responseDto = null;
            Guid guid = new Guid(sharingId);

            if (sharingId.Equals(null))
            {
                responseDto = new BaseResponseDto
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
                responseDto = new BaseResponseDto
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

            responseDto = new BaseResponseDto
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


        public BaseResponseDto Delete(string sharingId)
        {
            BaseResponseDto responseDto = null;
            Guid guid = new Guid(sharingId);

            if (sharingId.Equals(null))
            {
                responseDto = new BaseResponseDto
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
                responseDto = new BaseResponseDto
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

            responseDto = new BaseResponseDto
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
