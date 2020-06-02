﻿using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.Models;
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


        public string Login(UserLoginModel user)
        {
            Users loggedUser = _uow
                .GetRepository<Users>()
                .GetAll()
                .SingleOrDefault(u => u.Email == user.Email);
            if (loggedUser == null)
            {
                return "Incorrect Email or Password";
            } else if (loggedUser.Password != user.Password)
            {
                return "Incorrect Email or Password";
            }
            return "Logged in";
        }

        public string Register(UserRegisterModel user)
        {
            if(user == null)
            {
                return "Register failed";
            }

            Users newUser = new Users
            {
                UserId = Guid.NewGuid(),
                Email = user.Email,
                Password = user.Password,
                Fullname = user.Fullname,
                Phone = user.Phone,
                IsMentor = false,
                YearOfBirth = user.YearOfBirth,
                AvatarUrl = null
            };
            _uow.GetRepository<Users>().Insert(newUser);
            _uow.Commit();

            return "Success";
        }
    }
}