using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO.ResponseDtos;
using Domain.Helper.DataObjects;
using Domain.Helper.HelperFunctions;
using Domain.Models.ChannelModels;
using Domain.Models.EnrollModels;
using Domain.Models.MenteeModels;
using Domain.Models.SharingModels;
using Domain.Models.SubscriptionModels;
using Domain.Models.UserModels;
using Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class MenteeService : IMenteeService
    {
        #region Classes and Constructor
        protected readonly IUnitOfWork _uow;
        protected readonly IOptions<AppSetting> _options;
        protected TokenManager tokenManager;

        public MenteeService(IUnitOfWork uow, IOptions<AppSetting> options)
        {
            _uow = uow;
            _options = options;
            tokenManager = new TokenManager(_options);
        }
        #endregion Classes and Constructor



        #region CRUD API Methods

        public BaseResponseDto<MenteeViewModel> GetAll()
        {
            BaseResponseDto<MenteeViewModel> responseDto = new BaseResponseDto<MenteeViewModel>
            {
                Status = 0,
                Message = "Success",
                Content = null
            };

            IEnumerable<MenteeViewModel> result = null;

            try
            {
                result = _uow
                .GetRepository<Mentee>()
                .GetAll()
                .Include(m => m.User)
                .Select(m => new MenteeViewModel
                {
                    MenteeId = m.MenteeId,
                    Email = m.User.Email,
                    Fullname = m.User.Fullname,
                    Description = m.User.Description,
                    AvatarUrl = m.User.AvatarUrl
                });
            }
            catch (Exception e)
            {
                throw e;
            }

            if (result == null)
            {
                responseDto.Status = 1;
                responseDto.Message = "There are no users in the system";
            };

            //finalize
            responseDto.Content = result;
            return responseDto;
        }


        public BaseResponseDto<MenteeModel> GetById(string menteeId)
        {
            IEnumerable<MenteeModel> result = null;
            BaseResponseDto<MenteeModel> responseDto = new BaseResponseDto<MenteeModel>
            {
                Status = 0,
                Message = "Success",
                Content = null
            };

            if (menteeId == null)
            {
                responseDto = new BaseResponseDto<MenteeModel>
                {
                    Status = 1,
                    Message = "MentorId must be specified",
                    Content = null
                };
                return responseDto;
            };

            try
            {
                result = _uow
                .GetRepository<Mentee>()
                .GetAll()

                .Include(m => m.User)
                .Include(m => m.Subscription)
                .ThenInclude(s => s.Channel)
                .ThenInclude(c => c.Mentor)
                .ThenInclude(m => m.User)
                .Include(m => m.Subscription)
                .ThenInclude(s => s.Channel)
                .ThenInclude(c => c.Topic)

                .Where(m => m.MenteeId == new Guid(menteeId))
                .Select(m => new MenteeModel
                {
                    MenteeId = m.MenteeId,
                    User = new UserViewModel
                    {
                        UserId = m.User.UserId,
                        Email = m.User.Email,
                        Fullname = m.User.Fullname,
                        Description = m.User.Description,
                        Phone = m.User.Phone,
                        AvatarUrl = m.User.AvatarUrl,
                        Balance = m.User.Balance,
                        YearOfBirth = m.User.YearOfBirth
                    },
                    Subscription = m.Subscription.Select(s => new SubscriptionViewModel
                    {
                        SubscriptionId = s.SubscriptionId,
                        MenteeName = m.User.Email,
                        ChannelMentorId = s.Channel.Mentor.MentorId,
                        ChannelMentor = s.Channel.Mentor.User.Email,
                        ChannelTopicId = s.Channel.Topic.TopicId,
                        ChannelTopic = s.Channel.Topic.TopicName,
                        TimeSubscripted = s.TimeSubscripted,
                        IsDisable = s.IsDisable
                    }).ToList(),
                    IsDisable = m.IsDisable
                });
            }
            catch (Exception e)
            {
                throw e;
            }

            if (result == null)
            {
                responseDto = new BaseResponseDto<MenteeModel>
                {
                    Status = 2,
                    Message = "Mentor with id " + menteeId + " does not exist",
                    Content = null
                };
                return responseDto;
            }

            responseDto.Content = result;

            return responseDto;
        }


        public BaseResponseDto GoogleLogin(string menteeEmail)
        {
            UserAuthModel result = null;
            BaseResponseDto responseDto = null;

            #region Check input
            if (menteeEmail == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 1,
                    Message = "Email must be specified"
                };
                return responseDto;
            };

            try
            {
                result = _uow
                .GetRepository<Mentee>()
                .GetAll()
                .Include(m => m.User)
                .Where(m => m.User.Email == menteeEmail)
                .Select(m => new UserAuthModel
                {
                    Id = m.MenteeId,
                    Email = menteeEmail,
                    RoleName = "mentee"
                })
                .FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }

            if (result == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 2,
                    Message = "Mentee " + menteeEmail + " does not exist",
                };
                return responseDto;
            }
            #endregion

            #region Generate JWT
            //  not in used - yet
            //  string jwtToken = tokenManager.CreateUserAccessToken(result);
            #endregion

            //  finalize
            responseDto = new BaseResponseDto
            {
                Status = 0,
                Message = result.Id.ToString() // jwtToken
            };
            return responseDto;
        }


        public BaseResponseDto Insert(MenteeInsertModel menteeInsertModel)
        {
            BaseResponseDto responseDto = null;

            #region Check input
            if (menteeInsertModel == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 1,
                    Message = "Faulthy mentee info"
                };
                return responseDto;
            }
            #endregion

            #region Insert mentee into database
            Guid userId = Guid.NewGuid();
            Guid menteeId = Guid.NewGuid();
            try
            {
                Mentee newMentee = new Mentee
                {
                    MenteeId = menteeId,
                    User = new User
                    {
                        UserId = userId,
                        Email = menteeInsertModel.User.Email,
                        Fullname = menteeInsertModel.User.Fullname,
                        YearOfBirth = menteeInsertModel.User.YearOfBirth,
                        AvatarUrl = menteeInsertModel.User.AvatarUrl,
                        Balance = 0,
                        Phone = menteeInsertModel.User.Phone,
                        Description = ""
                    },
                    UserId = userId,
                    IsDisable = false
                };

                _uow.GetRepository<Mentee>().Insert(newMentee);
                _uow.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
            #endregion


            string jwtToken = tokenManager.CreateUserAccessToken(new UserAuthModel
            {
                Id = menteeId,
                Email = menteeInsertModel.User.Email,
                RoleName = "mentee"
            });


            responseDto = new BaseResponseDto
            {
                Status = 0,
                Message = jwtToken
            };

            return responseDto;
        }


        public BaseResponseDto Update(MenteeUpdateModel menteeUpdateModel)
        {
            BaseResponseDto responseDto = null;

            if (menteeUpdateModel == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 1,
                    Message = "Faulthy mentee info"
                };
                return responseDto;
            }

            Mentee existingMentee = null;

            try
            {
                existingMentee = _uow.GetRepository<Mentee>()
                    .GetAll()
                    .Include(m => m.User)
                    .FirstOrDefault(m => m.MenteeId == menteeUpdateModel.MenteeId);
            }
            catch (Exception e)
            {
                throw e;
            }

            if (existingMentee == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 2,
                    Message = "No existing mentor with specified id found"
                };
                return responseDto;
            }

            existingMentee.User.Email = menteeUpdateModel.User.Email;
            existingMentee.User.Phone = menteeUpdateModel.User.Phone;
            existingMentee.User.Fullname = menteeUpdateModel.User.Fullname;
            existingMentee.User.YearOfBirth = menteeUpdateModel.User.YearOfBirth;
            existingMentee.User.AvatarUrl = menteeUpdateModel.User.AvatarUrl;
            existingMentee.User.Balance = menteeUpdateModel.User.Balance;
            existingMentee.User.Description = menteeUpdateModel.User.Description;

            try
            {
                _uow.GetRepository<Mentee>().Update(existingMentee);
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


        public BaseResponseDto ChangeStatus(string menteeId, bool status)
        {
            BaseResponseDto responseDto = null;
            Guid guid = new Guid(menteeId);

            if (menteeId.Equals(null))
            {
                responseDto = new BaseResponseDto
                {
                    Status = 1,
                    Message = "Faulthy mentee Id."
                };
                return responseDto;
            }

            Mentee existingMentee = _uow
                .GetRepository<Mentee>()
                .GetAll()
                .FirstOrDefault(m => m.MenteeId == guid);
            if (existingMentee == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 2,
                    Message = "Mentee with specified id not found"
                };
                return responseDto;
            }

            try
            {
                existingMentee.IsDisable = status;
                _uow.GetRepository<Mentee>().Update(existingMentee);
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
                responseDto.Message = "Mentee is disabled.";
            }
            else if (status == false)
            {
                responseDto.Message = "Mentee is enabled.";
            }

            return responseDto;
        }


        public BaseResponseDto Delete(string menteeId)
        {
            BaseResponseDto responseDto = null;
            Guid guid = new Guid(menteeId);

            if (menteeId.Equals(null))
            {
                responseDto = new BaseResponseDto
                {
                    Status = 1,
                    Message = "Faulthy mentee Id."
                };
                return responseDto;
            }

            Mentee existingMentee = _uow
                .GetRepository<Mentee>()
                .GetAll()
                .FirstOrDefault(m => m.MenteeId == guid);
            if (existingMentee == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 2,
                    Message = "Mentor with specified id not found"
                };
                return responseDto;
            }

            try
            {
                _uow.GetRepository<Mentee>().Delete(existingMentee);
                _uow.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            responseDto = new BaseResponseDto
            {
                Status = 0,
                Message = "Successfully removed mentee " + existingMentee.MenteeId + " from database."
            };

            return responseDto;
        }


        #endregion


        #region Specialized Methods


        public BaseResponseDto<MenteeSubbedChannelModel> GetSubbedChannels(string menteeId)
        {
            IEnumerable<MenteeSubbedChannelModel> result = null;
            BaseResponseDto<MenteeSubbedChannelModel> responseDto 
                = new BaseResponseDto<MenteeSubbedChannelModel>
            {
                Status = 0,
                Message = "Success",
                Content = null
            };

            if (menteeId == null)
            {
                responseDto = new BaseResponseDto<MenteeSubbedChannelModel>
                {
                    Status = 1,
                    Message = "MentorId must be specified",
                    Content = null
                };
                return responseDto;
            };

            try
            {
                result = _uow
                .GetRepository<Mentee>()
                .GetAll()

                .Include(m => m.Subscription)
                .ThenInclude(m => m.Channel)
                .ThenInclude(m => m.Mentor)
                .ThenInclude(m => m.User)
                .Include(m => m.Subscription)
                .ThenInclude(m => m.Channel)
                .ThenInclude(m => m.Topic)

                .Where(m => m.MenteeId == new Guid(menteeId))
                .Select(m => new MenteeSubbedChannelModel
                {
                    MenteeId = m.MenteeId,
                    User = new UserViewModel
                    {
                        UserId = m.User.UserId,
                        Email = m.User.Email,
                        Fullname = m.User.Fullname,
                        Description = m.User.Description,
                        Phone = m.User.Phone,
                        AvatarUrl = m.User.AvatarUrl,
                        Balance = m.User.Balance,
                        YearOfBirth = m.User.YearOfBirth
                    },
                    Channels = m.Subscription.Select(s => new ChannelViewModel
                    {
                        ChannelId = s.ChannelId,
                        MentorName = s.Channel.Mentor.User.Email,
                        TopicName = s.Channel.Topic.TopicName
                    }).ToList()
                });
            }
            catch (Exception e)
            {
                throw e;
            }

            if (result == null)
            {
                responseDto = new BaseResponseDto<MenteeSubbedChannelModel>
                {
                    Status = 2,
                    Message = "Mentor with id " + menteeId + " does not exist",
                    Content = null
                };
                return responseDto;
            }

            responseDto.Content = result;

            return responseDto;
        }


        public BaseResponseDto<MenteeEnrolledSharingModel> GetEnrolledSharings(string menteeId)
        {
            IEnumerable<MenteeEnrolledSharingModel> result = null;
            BaseResponseDto<MenteeEnrolledSharingModel> responseDto = new BaseResponseDto<MenteeEnrolledSharingModel>
                {
                    Status = 0,
                    Message = "Success",
                    Content = null
                };

            if (menteeId == null)
            {
                responseDto = new BaseResponseDto<MenteeEnrolledSharingModel>
                {
                    Status = 1,
                    Message = "MentorId must be specified",
                    Content = null
                };
                return responseDto;
            };

            try
            {
                result = _uow
                .GetRepository<Mentee>()
                .GetAll()

                .Include(m => m.Subscription)
                .ThenInclude(m => m.Enroll)
                .ThenInclude(m => m.Sharing)

                .Where(m => m.MenteeId == new Guid(menteeId))
                .Select(m => new MenteeEnrolledSharingModel
                {
                    MenteeId = m.MenteeId,
                    User = new UserViewModel
                    {
                        UserId = m.User.UserId,
                        Email = m.User.Email,
                        Fullname = m.User.Fullname,
                        Description = m.User.Description,
                        Phone = m.User.Phone,
                        AvatarUrl = m.User.AvatarUrl,
                        Balance = m.User.Balance,
                        YearOfBirth = m.User.YearOfBirth
                    },
                    Subscriptions = m.Subscription.Select(m => new SubscriptionSharingListModel
                    {
                        SubscriptionId = m.SubscriptionId,
                        Sharings = m.Enroll.Select(m => new SharingViewModel 
                        {
                            SharingId = m.Sharing.SharingId,
                            SharingName = m.Sharing.SharingName,
                            Price = m.Sharing.Price,
                            MentorName = m.Sharing.Channel.Mentor.User.Fullname,
                            StartTime = m.Sharing.StartTime,
                            EndTime = m.Sharing.EndTime,
                            ImageUrl = m.Sharing.ImageUrl,
                            IsApproved = m.Sharing.IsApproved
                        }).ToList()
                    }).ToList()
                });
            }
            catch (Exception e)
            {
                throw e;
            }

            if (result == null)
            {
                responseDto = new BaseResponseDto<MenteeEnrolledSharingModel>
                {
                    Status = 2,
                    Message = "Mentor with id " + menteeId + " does not exist",
                    Content = null
                };
                return responseDto;
            }

            responseDto.Content = result;

            return responseDto;
        }



        public BaseResponseDto<MenteeEnrollCountModel> CountEnroll()
        {
            BaseResponseDto<MenteeEnrollCountModel> responseDto = new BaseResponseDto<MenteeEnrollCountModel>
            {
                Status = 0,
                Message = "Success",
                Content = null
            };

            List<MenteeEnrollCountModel> result = new List<MenteeEnrollCountModel>();
            List<Mentee> menteeList = new List<Mentee>();

            try
            {
                menteeList = _uow
                    .GetRepository<Mentee>()
                    .GetAll()

                    .Include(m => m.User)
                    .Include(m => m.Subscription)
                    .ThenInclude(m => m.Enroll)

                    .ToList();

                foreach (Mentee mentee in menteeList)
                {
                    int count = 0;

                    foreach (Subscription subscription in mentee.Subscription)
                    {
                        foreach (Enroll enroll in subscription.Enroll)
                        {
                            count++;
                        }
                    }

                    MenteeEnrollCountModel model = new MenteeEnrollCountModel 
                    {
                        MenteeId = mentee.MenteeId,
                        MenteeEmail = mentee.User.Email,
                        EnrollCount = count
                    };
                    result.Add(model);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            if (result == null)
            {
                responseDto.Status = 1;
                responseDto.Message = "There are no topic in the system";
            };

            //finalize
            responseDto.Content = result;
            return responseDto;
        }


        #endregion
    }
}
