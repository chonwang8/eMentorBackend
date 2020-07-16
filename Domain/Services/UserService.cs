﻿using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;

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



        public IEnumerable<UserViewModel> GetAll()
        {
            IEnumerable<UserViewModel> result = _uow
                .GetRepository<User>()
                .GetAll()
                .Select(u => new UserViewModel
                {
                    UserId = u.UserId,
                    Email = u.Email,
                    Fullname = u.Fullname,
                    YearOfBirth = u.YearOfBirth,
                    Phone = u.Phone,
                    AvatarUrl = u.AvatarUrl,
                    Balance = u.Balance,
                    Description = u.Description
                });

            return result;
        }

        public IEnumerable<UserViewModel> GetById(string userId)
        {
            if (userId == null)
            {
                return null;
            }

            IEnumerable<UserViewModel> result = _uow
                .GetRepository<User>()
                .GetAll()
                .Where(u => u.UserId.Equals(new Guid(userId)))
                .Select(u => new UserViewModel
                {
                    UserId = u.UserId,
                    Email = u.Email,
                    Fullname = u.Fullname,
                    YearOfBirth = u.YearOfBirth,
                    Phone = u.Phone,
                    AvatarUrl = u.AvatarUrl,
                    Balance = u.Balance,
                    Description = u.Description
                });

            return result;
        }

        public int Insert(UserInsertModel userInsert)
        {
            int result = 0;

            //  Check input
            if (userInsert == null)
            {
                result = 0;
                return result;
            }


            //  Check existing user
            User userInDb = _uow
                .GetRepository<User>()
                .GetAll()
                .SingleOrDefault(u => u.Email == userInsert.Email);
            if (userInDb != null)
            {
                result = 1;
                return result;
            }


            //  Create new User
            User user = new User
            {
                UserId = Guid.NewGuid(),
                Email = userInsert.Email,
                Fullname = userInsert.Fullname,
                Phone = userInsert.Phone,
                AvatarUrl = "default",
                Balance = 0,
                Description = "",
                YearOfBirth = 1999,
                IsDisable = false
            };

            //  Insert user to DB
            try
            {
                _uow.GetRepository<User>().Insert(user);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public int Update(UserViewModel user)
        {
            int result = 0;

            if (user == null)
            {
                result = 0;
                return result;
            }

            User existingUser = _uow.GetRepository<User>()
                .GetAll()
                .FirstOrDefault(u => u.UserId == user.UserId);

            if (existingUser == null)
            {
                result = 1;
                return result;
            }

            User updateUser = new User
            {
                UserId = existingUser.UserId,
                Email = user.Email,
                Fullname = user.Fullname,
                Phone = user.Phone,
                AvatarUrl = user.AvatarUrl,
                Balance = user.Balance,
                Description = user.Description,
                YearOfBirth = user.YearOfBirth
            };

            try
            {
                _uow.GetRepository<User>().Update(updateUser);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }


            return result;
        }

        public int ChangeStatus(string userId, bool status)
        {
            int result = 0;
            Guid guid = new Guid(userId);

            if (userId.Equals(null))
            {
                result = 0;
                return result;
            }

            User existingUser = _uow
                .GetRepository<User>()
                .GetAll()
                .FirstOrDefault(m => m.UserId == guid);
            if (existingUser == null)
            {
                result = 1;
                return result;
            }

            existingUser.IsDisable = status;

            try
            {
                _uow.GetRepository<User>().Update(existingUser);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public int Delete(string userId)
        {
            int result = 0;
            Guid guid = new Guid(userId);

            if (userId.Equals(null))
            {
                result = 0;
                return result;
            }

            User existingUser = _uow
                .GetRepository<User>()
                .GetAll()
                .FirstOrDefault(u => u.UserId == guid);
            if (existingUser == null)
            {
                result = 1;
                return result;
            }

            try
            {
                _uow.GetRepository<User>().Delete(existingUser);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }


            return result;
        }


    }
}
