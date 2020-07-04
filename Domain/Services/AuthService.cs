using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO.AuthDTOs;
using Domain.Helper.DataObjects;
using Domain.Helper.HelperFunctions;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using Domain.ViewModels.UserModels;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Domain.Services
{
    public class AuthService : IAuthService
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IOptions<AppSetting> _options;
        protected TokenManager tokenManager;

        public AuthService(IUnitOfWork uow, IOptions<AppSetting> options)
        {
            _uow = uow;
            _options = options;
            tokenManager = new TokenManager(_options);
        }

        public string Register(UserRegisterViewModel user)
        {
            if (user == null)
            {
                return "Register failed";
            }

            User newUser = new User
            {
                UserId = Guid.NewGuid(),
                Email = user.Email,
                Fullname = user.Fullname,
                Phone = user.Phone,
                YearOfBirth = user.YearOfBirth,
                AvatarUrl = "default"
            };
            _uow.GetRepository<User>().Insert(newUser);
            _uow.Commit();

            return "Success";
        }


        public string Login(AdminLoginViewModel adminLogin)
        {
            if (adminLogin == null)
            {
                return "Invalid Login info";
            }

            Admin loggedAdmin = _uow
                .GetRepository<Admin>()
                .GetAll()
                .SingleOrDefault(a => a.AdminUsername == adminLogin.AdminUsername);
            if (loggedAdmin != null)
            {
                if (loggedAdmin.Password != adminLogin.Password)
                    return null;
            }

            string jwtToken = tokenManager.CreateAdminAccessToken(new AdminViewModel
            {
                AdminId = loggedAdmin.AdminId,
                AdminUsername = loggedAdmin.AdminUsername
            });

            return jwtToken;
        }


        public LoginResponseDTO Login(UserLoginViewModel user)
        {
            LoginResponseDTO result = null;

            #region Check Input
            if (user == null)
            {
                result = new LoginResponseDTO
                {
                    Status = 1,
                    Message = "User Login information must not be null",
                    Result = null
                };
                return result;
            }

            if (user.RoleName != "mentor" && user.RoleName != "mentee")
            {

                result = new LoginResponseDTO
                {
                    Status = 2,
                    Message = "User missing required role",
                    Result = null
                };
                return result;
            }
            #endregion 

            #region Check User
            User loggedUser = _uow
                .GetRepository<User>()
                .GetAll()
                .SingleOrDefault(u => u.Email == user.Email);

            if (loggedUser == null)
            {
                result = new LoginResponseDTO
                {
                    Status = 3,
                    Result = null,
                    Message = "Email does not exist ! Please Register !"
                };
                return result;
            }
            #endregion

            #region Check Role

            if (user.RoleName == "mentor")
            {
                Mentor mentor = _uow
                    .GetRepository<Mentor>()
                    .GetAll()
                    .SingleOrDefault(m => m.UserId == loggedUser.UserId);
                if (mentor == null)
                {
                    result = new LoginResponseDTO
                    {
                        Status = 4,
                        Result = null,
                        Message = "This user is not a Mentor."
                    };
                    return result;
                }
            }
            else if (user.RoleName == "mentee")
            {
                Mentee mentee = _uow
                    .GetRepository<Mentee>()
                    .GetAll()
                    .SingleOrDefault(m => m.UserId == loggedUser.UserId);
                if (mentee == null)
                {
                    result = new LoginResponseDTO
                    {
                        Status = 4,
                        Result = null,
                        Message = "This user is not a Mentee."
                    };
                    return result;
                }
            }

            #endregion

            string jwtToken = tokenManager.CreateUserAccessToken(new UserRoleViewModel
            {
                UserId = loggedUser.UserId,
                Email = loggedUser.Email,
                RoleName = user.RoleName
            });

            result = new LoginResponseDTO
            {
                Status = 0,
                Message = "Login Successfully.",
                Result = jwtToken
            };

            return result;
        }


    }
}
