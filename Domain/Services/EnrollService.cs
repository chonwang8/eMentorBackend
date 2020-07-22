using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.Models.EnrollModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.DTO.ResponseDtos;
using Microsoft.EntityFrameworkCore;
using Domain.Models.SharingModels;
using Domain.Models.SubscriptionModels;

namespace Domain.Services
{
    public class EnrollService : IEnrollService
    {
        #region Classes and Constructor
        protected readonly IUnitOfWork _uow;

        public EnrollService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        #endregion Classes and Constructor



        public BaseResponseDto<EnrollViewModel> GetAll()
        {
            BaseResponseDto<EnrollViewModel> responseDto =
                new BaseResponseDto<EnrollViewModel>
                {
                    Status = 0,
                    Message = "Success",
                    Content = null
                };

            IEnumerable<EnrollViewModel> result = null;

            try
            {
                result = _uow
                .GetRepository<Enroll>()
                .GetAll()
                
                .Include(e => e.Sharing)
                .ThenInclude(e => e.Channel)
                .ThenInclude(e => e.Mentor)
                .ThenInclude(e => e.User)
                .Include(e => e.Subscription)
                .ThenInclude(e => e.Mentee)
                .ThenInclude(e => e.User)

                .Select(e => new EnrollViewModel
                {
                    EnrollId = e.EnrollId,
                    SubscriptionId = e.SubscriptionId,
                    SharingId = e.Sharing.SharingId,
                    SharingName = e.Sharing.SharingName,
                    MenteeName = e.Subscription.Mentee.User.Email,
                    MentorName = e.Sharing.Channel.Mentor.User.Email,
                    IsDisable = e.IsDisable
                });
            }
            catch (Exception e)
            {
                throw e;
            }

            if (result == null)
            {
                responseDto.Status = 1;
                responseDto.Message = "There are no enrolls in the system";
            };

            //  finalize
            responseDto.Content = result;
            return responseDto;
        }

        public BaseResponseDto<EnrollModel> GetById(string enrollId)
        {
            IEnumerable<EnrollModel> result = null;
            BaseResponseDto<EnrollModel> responseDto = new BaseResponseDto<EnrollModel>
            {
                Status = 0,
                Message = "Success",
                Content = null
            };

            if (enrollId == null)
            {
                responseDto = new BaseResponseDto<EnrollModel>
                {
                    Status = 1,
                    Message = "EnrollId must be specified",
                    Content = null
                };
                return responseDto;
            };

            try
            {
                result = _uow
                .GetRepository<Enroll>()
                .GetAll()

                .Include(e => e.Sharing)
                .ThenInclude(e => e.Channel)
                .ThenInclude(e => e.Mentor)
                .ThenInclude(e => e.User)
                .Include(e => e.Subscription)
                .ThenInclude(e => e.Mentee)
                .ThenInclude(e => e.User)

                .Where(e => e.EnrollId.Equals(new Guid(enrollId)))
                .Select(e => new EnrollModel
                {
                    EnrollId = e.EnrollId,
                    Sharing = new SharingViewModel
                    {
                        SharingId = e.SharingId,
                        SharingName = e.Sharing.SharingName,
                        MentorName = e.Sharing.Channel.Mentor.User.Email,
                        StartTime = e.Sharing.StartTime,
                        EndTime = e.Sharing.EndTime,
                        Price = e.Sharing.Price,
                        ImageUrl = e.Sharing.ImageUrl,
                        IsApproved = e.Sharing.IsApproved
                    },
                    Subscription = new SubscriptionViewModel
                    {
                        SubscriptionId = e.SubscriptionId,
                        MenteeName = e.Subscription.Mentee.User.Email,
                        ChannelMentor = e.Sharing.Channel.Mentor.User.Email,
                        ChannelTopic = e.Sharing.Channel.Topic.TopicName,
                        TimeSubscripted = e.Subscription.TimeSubscripted,
                        IsDisable = e.Subscription.IsDisable
                    },
                    IsDisable = e.IsDisable
                });
            }
            catch (Exception e)
            {
                throw e;
            }

            if (result == null)
            {
                responseDto = new BaseResponseDto<EnrollModel>
                {
                    Status = 2,
                    Message = "Enroll with id " + enrollId + " does not exist",
                    Content = null
                };
                return responseDto;
            }

            responseDto.Content = result;

            return responseDto;
        }

        public BaseResponseDto Insert(EnrollInsertModel enrollInsertModel)
        {
            BaseResponseDto responseDto = null;

            if (enrollInsertModel == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 1,
                    Message = "Faulthy enroll info"
                };
                return responseDto;
            }

            Enroll existingEnroll = null;

            try
            {
                existingEnroll = _uow
                    .GetRepository<Enroll>()
                    .GetAll()
                    .FirstOrDefault(e => 
                    e.SubscriptionId == enrollInsertModel.SubscriptionId && 
                    e.SharingId == enrollInsertModel.SharingId);
            }
            catch (Exception e)
            {
                throw e;
            }

            if (existingEnroll != null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 2,
                    Message = "Already enrolled to this sharing"
                };
                return responseDto;
            }

            try
            {
                Enroll newEnroll = new Enroll
                {
                    EnrollId = Guid.NewGuid(),
                    SharingId = enrollInsertModel.SharingId,
                    SubscriptionId = enrollInsertModel.SubscriptionId,
                    IsDisable = false
                };

                _uow.GetRepository<Enroll>().Insert(newEnroll);
                _uow.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            responseDto = new BaseResponseDto
            {
                Status = 0,
                Message = "Successfully enrolled to sharing"
            };

            return responseDto;
        }

        public BaseResponseDto Update(EnrollUpdateModel enrollUpdateModel)
        {
            BaseResponseDto responseDto = null;

            if (enrollUpdateModel == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 1,
                    Message = "Faulthy enroll info"
                };
                return responseDto;
            }

            Enroll existingEnroll = null;

            try
            {
                existingEnroll = _uow.GetRepository<Enroll>()
                    .GetAll()
                    .FirstOrDefault(e => e.EnrollId == enrollUpdateModel.EnrollId);
            }
            catch (Exception e)
            {
                throw e;
            }

            if (existingEnroll == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 2,
                    Message = "Not enrolled to sharing yet"
                };
                return responseDto;
            }

            existingEnroll.SharingId = enrollUpdateModel.SharingId;
            existingEnroll.SubscriptionId = enrollUpdateModel.SubscriptionId;
            existingEnroll.IsDisable = enrollUpdateModel.IsDisable;

            try
            {
                _uow.GetRepository<Enroll>().Update(existingEnroll);
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

        public BaseResponseDto ChangeStatus(string enrollId, bool status)
        {
            BaseResponseDto responseDto = null;
            Guid guid = new Guid(enrollId);

            if (enrollId.Equals(null))
            {
                responseDto = new BaseResponseDto
                {
                    Status = 1,
                    Message = "Faulthy enroll Id."
                };
                return responseDto;
            }

            Enroll existingEnroll = _uow
                .GetRepository<Enroll>()
                .GetAll()
                .FirstOrDefault(e => e.EnrollId == guid);
            if (existingEnroll == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 2,
                    Message = "Enroll with specified id not found"
                };
                return responseDto;
            }

            try
            {
                existingEnroll.IsDisable = status;
                _uow.GetRepository<Enroll>().Update(existingEnroll);
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
                responseDto.Message = "Enroll is disabled.";
            }
            else if (status == false)
            {
                responseDto.Message = "Enroll is enabled.";
            }

            return responseDto;
        }

        public BaseResponseDto Delete(string enrollId)
        {
            BaseResponseDto responseDto = null;
            Guid guid = new Guid(enrollId);

            if (enrollId.Equals(null))
            {
                responseDto = new BaseResponseDto
                {
                    Status = 1,
                    Message = "Faulthy enroll Id."
                };
                return responseDto;
            }

            Enroll existingEnroll = _uow
                .GetRepository<Enroll>()
                .GetAll()
                .FirstOrDefault(e => e.EnrollId == guid);
            if (existingEnroll == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 2,
                    Message = "Enroll with specified id not found"
                };
                return responseDto;
            }

            try
            {
                _uow.GetRepository<Enroll>().Delete(existingEnroll);
                _uow.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            responseDto = new BaseResponseDto
            {
                Status = 0,
                Message = "Successfully remove channel " + existingEnroll.EnrollId + " from database."
            };

            return responseDto;
        }


    }
}
