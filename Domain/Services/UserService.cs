using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.ViewModels;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        #region Classes and Constructor

        protected readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        #endregion Classes and Constructor


        public string Login(UserLoginViewModel user)
        {
            User loggedUser = _uow
                .GetRepository<User>()
                .GetAll()
                .SingleOrDefault(u => u.Email == user.Email);
            if (loggedUser != null)
            {
                if (loggedUser.Password != user.Password)
                    return "Incorrect Password";
                return "Email does not exist ! Please Register !";
            }
            return "Logged in";
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
                Password = user.Password,
                Fullname = user.Fullname,
                Phone = user.Phone,
                YearOfBirth = user.YearOfBirth,
                AvatarUrl = "default"
            };
            _uow.GetRepository<User>().Insert(newUser);
            _uow.Commit();

            return "Success";
        }
    }
}
