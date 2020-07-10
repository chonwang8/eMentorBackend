using Domain.DTO;
using Domain.ViewModels.EnrollModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface IEnrollService
    {
        public IEnumerable<EnrollViewModel> GetAll();
        public IEnumerable<EnrollModel> GetById(string enrollId);
        public int Insert(EnrollInsertModel enrollModel);
        public int Update(EnrollModel enrollModel);
        public int ChangeStatus(string enrollId, bool status);
        public int Delete(string enrollId);
    }
}
