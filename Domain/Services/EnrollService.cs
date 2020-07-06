using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.ViewModels.EnrollModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Services
{
    public class EnrollService : IEnrollService
    {
        #region Classes and Constructor
        protected readonly IUnitOfWork _uow;

        public EnrollService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        #endregion Classes and Constructor



        public IEnumerable<EnrollViewModel> GetAll(GetAllDTO request)
        {
            IEnumerable<EnrollViewModel> result = null;

            try
            {
                result = _uow
                    .GetRepository<Enroll>()
                    .GetAll()
                    .Select(e => new EnrollViewModel
                    {
                        EnrollId = e.EnrollId,
                        SharingId = e.SharingId,
                        SharingName = e.Sharing.SharingName,
                        SubscriptionId = e.SubscriptionId,
                        IsDisable = e.IsDisable
                    });
                result = result.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }

        public IEnumerable<EnrollModel> GetById(string enrollId)
        {
            if (enrollId == null)
            {
                return null;
            }

            IEnumerable<EnrollModel> result = null;

            try
            {
                result = _uow
                    .GetRepository<Enroll>()
                    .GetAll()
                    .Where(e => e.EnrollId.Equals(new Guid(enrollId)))
                    .Select(e => new EnrollModel
                    {
                        EnrollId = e.EnrollId,
                        SharingId = e.SharingId,
                        SubscriptionId = e.SubscriptionId,
                        IsDisable = e.IsDisable
                    });
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public int Insert(EnrollInsertModel enrollModel)
        {
            int result = 0;

            if (enrollModel == null)
            {
                result = 0;
                return result;
            }

            Enroll existingEnroll = _uow
                .GetRepository<Enroll>()
                .GetAll()
                .SingleOrDefault(e => e.SubscriptionId == enrollModel.SubscriptionId 
                && e.SharingId == enrollModel.SharingId);

            if (existingEnroll != null)
            {
                result = 1;
                return result;
            }

            Enroll enrollInsert = new Enroll
            {
                EnrollId = Guid.NewGuid(),
                SharingId = enrollModel.SharingId,
                SubscriptionId = enrollModel.SubscriptionId,
                IsDisable = false
            };

            try
            {
                _uow.GetRepository<Enroll>().Insert(enrollInsert);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public int Update(EnrollModel enrollModel)
        {
            int result = 0;

            if (enrollModel == null)
            {
                result = 0;
                return result;
            }


            Enroll existingEnroll = _uow
                .GetRepository<Enroll>()
                .GetAll()
                .SingleOrDefault(e => e.EnrollId == enrollModel.EnrollId);

            if (existingEnroll == null)
            {
                result = 1;
                return result;
            }


            Enroll enrollUpdate = new Enroll
            {
                EnrollId = enrollModel.EnrollId,
                SharingId = enrollModel.SharingId,
                SubscriptionId = enrollModel.SubscriptionId,
                IsDisable = enrollModel.IsDisable
            };

            try
            {
                _uow.GetRepository<Enroll>().Update(enrollUpdate);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public int ChangeStatus(string enrollId, bool status)
        {
            int result = 0;

            if (enrollId.Equals(null))
            {
                result = 0;
                return result;
            }

            Guid guid = new Guid(enrollId);

            Enroll existingEnroll = _uow
                .GetRepository<Enroll>()
                .GetAll()
                .FirstOrDefault(e => e.EnrollId == guid);

            if (existingEnroll == null)
            {
                result = 1;
                return result;
            }

            existingEnroll.IsDisable = status;

            try
            {
                _uow.GetRepository<Enroll>().Update(existingEnroll);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public int Delete(string enrollId)
        {
            int result = 0;

            if (enrollId.Equals(null))
            {
                result = 0;
                return result;
            }

            Guid guid = new Guid(enrollId);

            Enroll existingEnroll = _uow
                .GetRepository<Enroll>()
                .GetAll()
                .FirstOrDefault(e => e.EnrollId == guid);

            if (existingEnroll == null)
            {
                result = 1;
                return result;
            }

            try
            {
                _uow.GetRepository<Enroll>().Delete(existingEnroll);
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
