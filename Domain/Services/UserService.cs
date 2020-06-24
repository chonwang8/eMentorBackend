using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO;
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
    public class UserService : IUserService
    {
        #region Classes and Constructor
        protected readonly IUnitOfWork _uow;
        protected readonly IOptions<AppSetting> _options;
        protected TokenManager tokenManager;

        public UserService(IUnitOfWork uow, IOptions<AppSetting> options)
        {
            _uow = uow;
            _options = options;
            tokenManager = new TokenManager(_options);
        }

        #endregion Classes and Constructor




        #region RESTful API Functions

        //  Get all users
        public IEnumerable<UserViewModel> GetAll(GetAllDTO request)
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
            result = result.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);

            return result;
        }


        //  Get user by Id
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


        //  Add a user
        public int Insert(UserViewModel userInsertion)
        {
            int result = 0;

            //  Check input
            if (userInsertion == null)
            {
                result = 0;
                return result;
            }


            //  Check existing user
            User userInDb = _uow
                .GetRepository<User>()
                .GetAll()
                .SingleOrDefault(u => u.Email == userInsertion.Email);
            if (userInDb != null)
            {
                result = 1;
                return result;
            }


            //  Create new User
            User user = new User
            {
                UserId = Guid.NewGuid(),
                Email = userInsertion.Email,
                Fullname = userInsertion.Fullname,
                Phone = userInsertion.Phone,
                AvatarUrl = "default",
                Balance = 0,
                Description = userInsertion.Description,
                YearOfBirth = userInsertion.YearOfBirth
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


        //  Update a user
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

        //  Disable a user
        public int Disable(UserStatusViewModel user)
        {
            int result = 0;
            
            if (user.Equals(null))
            {
                result = 0;
                return result;
            }

            User existingUser = _uow
                .GetRepository<User>()
                .GetAll()
                .FirstOrDefault(u => u.UserId == user.UserId);
            if (existingUser == null)
            {
                result = 1;
                return result;
            }

            existingUser.IsDisable = user.IsDisable;

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


        //  Delete a user
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


        #endregion

    }
}
