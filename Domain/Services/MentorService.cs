using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.ViewModels.MentorModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class MentorService : IMentorService
    {

        #region Classes and Constructor
        protected readonly IUnitOfWork _uow;




        public MentorService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        #endregion Classes and Constructor




        #region RESTful API Functions

        public IEnumerable<MentorModel> GetAll(GetAllDTO request)
        {
            IEnumerable<MentorModel> result = _uow
                .GetRepository<Mentor>()
                .GetAll()
                .Select(u => new MentorModel
                {
                    MentorId = u.MentorId,
                    UserId = u.UserId
                });
            result = result.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            return result;
        }


        public IEnumerable<MentorModel> GetById(string mentorId)
        {
            if (mentorId == null)
            {
                return null;
            }

            IEnumerable<MentorModel> result = _uow
                .GetRepository<Mentor>()
                .GetAll()
                .Where(u => u.UserId.Equals(new Guid(mentorId)))
                .Select(u => new MentorModel
                {
                    MentorId = u.MentorId,
                    UserId = u.UserId
                });

            return result;
        }


        public int Insert(MentorModel mentor)
        {
            int result = 0;

            if (mentor == null)
            {
                result = 0;
                return result;
            }


            Mentor mentorInDb = _uow
                .GetRepository<Mentor>()
                .GetAll()
                .SingleOrDefault(u => u.MentorId == mentor.MentorId);
            if (mentorInDb != null)
            {
                result = 1;
                return result;
            }


            Mentor newMentor = new Mentor
            {
                MentorId = mentor.MentorId,
                UserId = mentor.UserId
            };


            try
            {
                _uow.GetRepository<Mentor>().Insert(newMentor);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }


        public int Update(MentorModel mentor)
        {
            int result = 0;

            if (mentor == null)
            {
                result = 0;
                return result;
            }

            Mentor existingMentor = _uow.GetRepository<Mentor>()
                .GetAll()
                .FirstOrDefault(u => u.MentorId == mentor.MentorId);

            if (existingMentor == null)
            {
                result = 1;
                return result;
            }

            existingMentor.UserId = mentor.UserId;

            try
            {
                _uow.GetRepository<Mentor>().Update(existingMentor);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }


            return result;
        }


        public int ChangeStatus(string mentorId, bool status)
        {
            int result = 0;
            Guid guid = new Guid(mentorId);

            if (mentorId.Equals(null))
            {
                result = 0;
                return result;
            }

            Mentor existingMentor = _uow
                .GetRepository<Mentor>()
                .GetAll()
                .FirstOrDefault(m => m.MentorId == guid);
            if (existingMentor == null)
            {
                result = 1;
                return result;
            }

            existingMentor.IsDisable = status;

            try
            {
                _uow.GetRepository<Mentor>().Update(existingMentor);
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
        public int Delete(string mentorId)
        {
            int result = 0;
            Guid guid = new Guid(mentorId);

            if (mentorId.Equals(null))
            {
                result = 0;
                return result;
            }

            Mentor existingMentor = _uow
                .GetRepository<Mentor>()
                .GetAll()
                .FirstOrDefault(m => m.MentorId == guid);
            if (existingMentor == null)
            {
                result = 1;
                return result;
            }

            try
            {
                _uow.GetRepository<Mentor>().Delete(existingMentor);
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
