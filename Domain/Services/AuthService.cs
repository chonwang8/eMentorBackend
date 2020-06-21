using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.Helper.DataObjects;
using Domain.Helper.HelperFunctions;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                    return "Incorrect username or password";
            }

            string jwtToken = tokenManager.CreateAdminAccessToken(new AdminViewModel
            {
                AdminId = loggedAdmin.AdminId,
                AdminUsername = loggedAdmin.AdminUsername
            });

            return jwtToken;
        }


        public string Login(UserLoginViewModel user)
        {
            if (user.RoleName != "mentor" || user.RoleName != "mentee")
            {
                return "Invalid Operation";
            }

            User loggedUser = _uow
                .GetRepository<User>()
                .GetAll()
                .SingleOrDefault(u => u.Email == user.Email);
            if (loggedUser != null)
            {
                return "Email does not exist ! Please Register !";
            }

            string jwtToken = tokenManager.CreateUserAccessToken(new UserRoleViewModel
            {
                UserId = loggedUser.UserId,
                Email = loggedUser.Email,
                Fullname = loggedUser.Fullname,
                RoleName = user.RoleName
            });

            return jwtToken;
        }


    }
}
