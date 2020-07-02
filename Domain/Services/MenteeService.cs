using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Services
{
    public class MenteeService : IMenteeService
    {
        #region Classes and Constructor
        protected readonly IUnitOfWork _uow;



        public MenteeService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        #endregion Classes and Constructor




        #region RESTful API Functions

        public IEnumerable<MenteeViewModel> GetAll(GetAllDTO request)
        {
            IEnumerable<MenteeViewModel> result = _uow
                .GetRepository<Mentee>()
                .GetAll()
                .Select(u => new MenteeViewModel
                {
                    MenteeId = u.MenteeId,
                    UserId = u.UserId,
                    IsDisable = u.IsDisable
                });

            result = result.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            return result;
        }


        public IEnumerable<MenteeViewModel> GetById(string menteeId)
        {
            if (menteeId == null)
            {
                return null;
            }

            IEnumerable<MenteeViewModel> result = _uow
                .GetRepository<Mentee>()
                .GetAll()
                .Where(u => u.UserId.Equals(new Guid(menteeId)))
                .Select(u => new MenteeViewModel
                {
                    MenteeId = u.MenteeId,
                    UserId = u.UserId,
                    IsDisable = u.IsDisable
                });

            return result;
        }


        public int Insert(MenteeViewModel mentee)
        {
            int result = 0;

            if (mentee == null)
            {
                result = 0;
                return result;
            }


            Mentee menteeInDb = _uow
                .GetRepository<Mentee>()
                .GetAll()
                .SingleOrDefault(u => u.MenteeId == mentee.MenteeId);
            if (menteeInDb != null)
            {
                result = 1;
                return result;
            }


            Mentee newMentee = new Mentee
            {
                MenteeId = mentee.MenteeId,
                UserId = mentee.UserId,
                IsDisable = mentee.IsDisable
            };


            try
            {
                _uow.GetRepository<Mentee>().Insert(newMentee);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }


        public int Update(MenteeViewModel mentee)
        {
            int result = 0;

            if (mentee == null)
            {
                result = 0;
                return result;
            }

            Mentee existingMentee = _uow.GetRepository<Mentee>()
                .GetAll()
                .FirstOrDefault(u => u.MenteeId == mentee.MenteeId);

            if (existingMentee == null)
            {
                result = 1;
                return result;
            }

            existingMentee.UserId = mentee.UserId;
            existingMentee.IsDisable = mentee.IsDisable;

            try
            {
                _uow.GetRepository<Mentee>().Update(existingMentee);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }


            return result;
        }


        public int ChangeStatus(string menteeId, bool status)
        {
            int result = 0;
            Guid guid = new Guid(menteeId);

            if (menteeId.Equals(null))
            {
                result = 0;
                return result;
            }

            Mentee existingMentee = _uow
                .GetRepository<Mentee>()
                .GetAll()
                .FirstOrDefault(m => m.MenteeId == guid);
            if (existingMentee == null)
            {
                result = 1;
                return result;
            }

            existingMentee.IsDisable = status;

            try
            {
                _uow.GetRepository<Mentee>().Update(existingMentee);
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
        public int Delete(string menteeId)
        {
            int result = 0;
            Guid guid = new Guid(menteeId);

            if (menteeId.Equals(null))
            {
                result = 0;
                return result;
            }

            Mentee existingMentee = _uow
                .GetRepository<Mentee>()
                .GetAll()
                .FirstOrDefault(m => m.MenteeId == guid);
            if (existingMentee == null)
            {
                result = 1;
                return result;
            }

            try
            {
                _uow.GetRepository<Mentee>().Delete(existingMentee);
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
