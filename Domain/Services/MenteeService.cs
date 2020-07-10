using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.ViewModels.MenteeModels;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<MenteeModel> GetAll()
        {
            IEnumerable<MenteeModel> result = _uow
                .GetRepository<Mentee>()
                .GetAll()
                .Select(u => new MenteeModel
                {
                    MenteeId = u.MenteeId,
                    UserId = u.UserId
                });

            return result;
        }


        public IEnumerable<MenteeModel> GetById(string menteeId)
        {
            if (menteeId == null)
            {
                return null;
            }

            IEnumerable<MenteeModel> result = _uow
                .GetRepository<Mentee>()
                .GetAll()
                .Where(u => u.UserId.Equals(new Guid(menteeId)))
                .Select(u => new MenteeModel
                {
                    MenteeId = u.MenteeId,
                    UserId = u.UserId
                });

            return result;
        }


        public int Insert(MenteeModel mentee)
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
                UserId = mentee.UserId
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


        public int Update(MenteeModel mentee)
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
