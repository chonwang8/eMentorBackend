﻿using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO.ResponseDtos;
using Domain.Helper.DataObjects;
using Domain.Helper.HelperFunctions;
using Domain.Services.Interfaces;
using Domain.Models.ChannelModels;
using Domain.Models.MentorModels;
using Domain.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models.RatingModels;

namespace Domain.Services
{
    public class MentorService : IMentorService
    {

        #region Classes and Constructor
        protected readonly IUnitOfWork _uow;
        protected readonly IOptions<AppSetting> _options;
        protected TokenManager tokenManager;

        public MentorService(IUnitOfWork uow, IOptions<AppSetting> options)
        {
            _uow = uow;
            _options = options;
            tokenManager = new TokenManager(_options);
        }
        #endregion Classes and Constructor



        #region CRUD Methods

        public BaseResponseDto<MentorViewModel> GetAll()
        {
            BaseResponseDto<MentorViewModel> responseDto = new BaseResponseDto<MentorViewModel>
            {
                Status = 0,
                Message = "Success",
                Content = null
            };

            IEnumerable<MentorViewModel> result = null;

            try
            {
                result = _uow
                .GetRepository<Mentor>()
                .GetAll()

                .Include(m => m.User)
                .Include(m => m.Rating)

                .Select(m => new MentorViewModel
                {
                    MentorId = m.MentorId,
                    Email = m.User.Email,
                    Fullname = m.User.Fullname,
                    Description = m.User.Description,
                    AvatarUrl = m.User.AvatarUrl,
                    Rating = new RatingViewModel
                    {
                        RatingScore = m.Rating.RatingScore
                    },
                    Channels = m.Channel.Select(c => new ChannelViewModel
                    {
                        ChannelId = c.ChannelId,
                        MentorName = m.User.Email,
                        TopicName = c.Topic.TopicName,
                        IsDisable = c.IsDisable
                    }).ToList()
                })
                .OrderByDescending(m => m.Rating.RatingScore);
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


        public BaseResponseDto<MentorModel> GetById(string mentorId)
        {
            IEnumerable<MentorModel> result = null;
            BaseResponseDto<MentorModel> responseDto = new BaseResponseDto<MentorModel>
            {
                Status = 0,
                Message = "Success",
                Content = null
            };

            if (mentorId == null)
            {
                responseDto = new BaseResponseDto<MentorModel>
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
                .GetRepository<Mentor>()
                .GetAll()

                .Include(m => m.User)
                .Include(m => m.Channel)

                .Where(m => m.MentorId == new Guid(mentorId))
                .Select(m => new MentorModel
                {
                    MentorId = m.MentorId,
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
                    Rating = new RatingViewModel
                    {
                        RatingScore = m.Rating.RatingScore
                    },
                    Channels = m.Channel.Select(c => new ChannelViewModel
                    {
                        ChannelId = c.ChannelId,
                        MentorName = m.User.Email,
                        TopicName = c.Topic.TopicName,
                        IsDisable = c.IsDisable
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
                responseDto = new BaseResponseDto<MentorModel>
                {
                    Status = 2,
                    Message = "Mentor with id " + mentorId + " does not exist",
                    Content = null
                };
                return responseDto;
            }

            responseDto.Content = result;

            return responseDto;
        }


        public BaseResponseDto GoogleLogin(string mentorEmail)
        {
            UserAuthModel result = null;
            BaseResponseDto responseDto = null;

            #region Check input
            if (mentorEmail == null)
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
                .GetRepository<Mentor>()
                .GetAll()
                .Include(m => m.User)
                .Where(m => m.User.Email == mentorEmail)
                .Select(m => new UserAuthModel
                {
                    Id = m.MentorId,
                    Email = mentorEmail,
                    RoleName = "mentor"
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
                    Message = "Mentor " + mentorEmail + " does not exist",
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
                //  Message = jwtToken
                Message = result.Id.ToString()
            };
            return responseDto;
        }


        public BaseResponseDto Insert(MentorInsertModel mentorInsertModel)
        {
            BaseResponseDto responseDto = null;

            #region Check input
            if (mentorInsertModel == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 1,
                    Message = "Faulthy mentor info"
                };
                return responseDto;
            }
            #endregion

            #region Insert mentor into database
            Guid userId = Guid.NewGuid();
            Guid mentorId = Guid.NewGuid();
            try
            {
                Mentor newMentor = new Mentor
                {
                    MentorId = mentorId,
                    UserId = userId,
                    User = new User
                    {
                        UserId = userId,
                        Email = mentorInsertModel.User.Email,
                        Fullname = mentorInsertModel.User.Fullname,
                        YearOfBirth = mentorInsertModel.User.YearOfBirth,
                        AvatarUrl = mentorInsertModel.User.AvatarUrl,
                        Balance = 0,
                        Phone = mentorInsertModel.User.Phone,
                        Description = ""
                    },
                    Rating = new Rating
                    {
                        MentorId = mentorId,
                        RatingCount = 0,
                        RatingScore = 0
                    },
                    IsDisable = false
                };

                _uow.GetRepository<Mentor>().Insert(newMentor);
                _uow.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
            #endregion


            string jwtToken = tokenManager.CreateUserAccessToken(new UserAuthModel
            {
                Id = mentorId,
                Email = mentorInsertModel.User.Email,
                RoleName = "mentor"
            });


            responseDto = new BaseResponseDto
            {
                Status = 0,
                Message = jwtToken
            };

            return responseDto;
        }


        public BaseResponseDto Update(MentorUpdateModel mentorUpdateModel)
        {
            BaseResponseDto responseDto = null;

            if (mentorUpdateModel == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 1,
                    Message = "Faulthy mentor info"
                };
                return responseDto;
            }

            Mentor existingMentor = null;

            try
            {
                existingMentor = _uow.GetRepository<Mentor>()
                    .GetAll()
                    .Include(m => m.User)
                    .FirstOrDefault(m => m.MentorId == mentorUpdateModel.MentorId);
            }
            catch (Exception e)
            {
                throw e;
            }

            if (existingMentor == null)
            {
                responseDto = new BaseResponseDto
                {
                    Status = 2,
                    Message = "No existing mentor with specified id found"
                };
                return responseDto;
            }

            existingMentor.User.Email = mentorUpdateModel.User.Email;
            existingMentor.User.Phone = mentorUpdateModel.User.Phone;
            existingMentor.User.Fullname = mentorUpdateModel.User.Fullname;
            existingMentor.User.YearOfBirth = mentorUpdateModel.User.YearOfBirth;
            existingMentor.User.AvatarUrl = mentorUpdateModel.User.AvatarUrl;
            existingMentor.User.Balance = mentorUpdateModel.User.Balance;
            existingMentor.User.Description = mentorUpdateModel.User.Description;

            try
            {
                _uow.GetRepository<Mentor>().Update(existingMentor);
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


        public BaseResponseDto ChangeStatus(string mentorId, bool status)
        {
            BaseResponseDto responseDto = null;
            Guid guid = new Guid(mentorId);

            if (mentorId.Equals(null))
            {
                responseDto = new BaseResponseDto
                {
                    Status = 1,
                    Message = "Faulthy mentor Id."
                };
                return responseDto;
            }

            Mentor existingMentor = _uow
                .GetRepository<Mentor>()
                .GetAll()
                .FirstOrDefault(m => m.MentorId == guid);
            if (existingMentor == null)
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
                existingMentor.IsDisable = status;
                _uow.GetRepository<Mentor>().Update(existingMentor);
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
                responseDto.Message = "Mentor is disabled.";
            }
            else if (status == false)
            {
                responseDto.Message = "Mentor is enabled.";
            }

            return responseDto;
        }


        public BaseResponseDto Delete(string mentorId)
        {
            BaseResponseDto responseDto = null;
            Guid guid = new Guid(mentorId);

            if (mentorId.Equals(null))
            {
                responseDto = new BaseResponseDto
                {
                    Status = 1,
                    Message = "Faulthy mentor Id."
                };
                return responseDto;
            }

            Mentor existingMentor = _uow
                .GetRepository<Mentor>()
                .GetAll()
                .FirstOrDefault(m => m.MentorId == guid);
            if (existingMentor == null)
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
                _uow.GetRepository<Mentor>().Delete(existingMentor);
                _uow.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            responseDto = new BaseResponseDto
            {
                Status = 0,
                Message = "Successfully removed mentor " + existingMentor.MentorId + " from database."
            };

            return responseDto;
        }


        #endregion



        #region Specialized Methods

        public BaseResponseDto InsertRating(RatingInsertModel ratingInsertModel)
        {
            BaseResponseDto responseDto = new BaseResponseDto
            {
                Status = 200,
                Message = null
            };

            #region Check input
            if (ratingInsertModel == null)
            {
                responseDto.Status = 400;
                responseDto.Message = "Faulthy input";
                return responseDto;
            }
            if (ratingInsertModel.RatingScore < 0 || ratingInsertModel.RatingScore > 5)
            {
                responseDto.Status = 400;
                responseDto.Message = "Rating is Base 5. Your input is not in Base 5";
                return responseDto;
            }

            Mentor existingMentor = _uow
                .GetRepository<Mentor>()
                .GetAll()

                .Include(m => m.Rating)

                .FirstOrDefault(m => m.MentorId == ratingInsertModel.MentorId);

            if (existingMentor == null)
            {
                responseDto.Status = 404;
                responseDto.Message = "Mentor Profile not found";
                return responseDto;
            }
            #endregion

            Rating mentorRating = existingMentor.Rating;
            double currentScore = mentorRating.RatingScore.Value;
            int currentCount = mentorRating.RatingCount.Value;
            int newCount = currentCount + 1;
            double calCurrentCount = (double) currentCount;
            double calNewCount = (double) newCount;
            try
            {
                mentorRating.RatingScore = 
                    ((currentScore * calCurrentCount) + ratingInsertModel.RatingScore) / calNewCount;
                mentorRating.RatingCount = newCount;
                _uow.GetRepository<Rating>().Update(mentorRating);
                _uow.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            responseDto.Status = 200;
            responseDto.Message = "Success";

            return responseDto;
        }

        public BaseResponseDto InsertRating(string mentorId)
        {
            BaseResponseDto responseDto = new BaseResponseDto
            {
                Status = 200,
                Message = null
            };
            Guid mentorGuid = new Guid(mentorId);


            #region Check input
            if (mentorId == null)
            {
                responseDto.Status = 400;
                responseDto.Message = "Faulthy input";
                return responseDto;
            }

            Mentor existingMentor = _uow
                .GetRepository<Mentor>()
                .GetAll()

                .Include(m => m.Rating)

                .FirstOrDefault(m => m.MentorId == mentorGuid);

            if (existingMentor == null)
            {
                responseDto.Status = 404;
                responseDto.Message = "Mentor Profile not found";
                return responseDto;
            }
            #endregion

            Rating mentorRating = new Rating
            { 
                MentorId = mentorGuid,
                RatingCount = 0,
                RatingScore = 0
            };
            try
            {
                _uow.GetRepository<Rating>().Insert(mentorRating);
                _uow.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            responseDto.Status = 200;
            responseDto.Message = "Success";

            return responseDto;
        }

        public IEnumerable<CountMenteeSubcribeTopicModel> CountMenteeEnrollSharing()
        {
            List<CountMenteeSubcribeTopicModel> result = null;

            List<Mentor> mentorList = _uow.GetRepository<Mentor>().GetAll().ToList();

            foreach (var mentor in mentorList)
            {
                int counter = _uow.GetRepository<Mentor>().GetAll()
                 .Include(m => m.Channel)
                 .ThenInclude(c => c.Sharing)
                 .ThenInclude(s => s.Enroll)
                 .Where(m => m.MentorId == mentor.MentorId)
                 .Count();

                result.Add(new CountMenteeSubcribeTopicModel
                {
                    MentorId = mentor.MentorId,
                    Counter = counter
                });
            }

            return result;
        }

        public IEnumerable<CountSharingByMentorModel> CountSharingByMentor()
        {
            List<CountSharingByMentorModel> result = null;

            List<Mentor> mentorList = _uow.GetRepository<Mentor>().GetAll().ToList();

            foreach (var mentor in mentorList)
            {
                int counter = _uow.GetRepository<Mentor>().GetAll()
                 .Include(m => m.Channel)
                 .ThenInclude(c => c.Sharing)
                 .Where(m => m.MentorId == mentor.MentorId)
                 .Count();

                result.Add(new CountSharingByMentorModel
                {
                    MentorId = mentor.MentorId,
                    Counter = counter
                });
            }

            return result;
        }


        #endregion
    }
}
