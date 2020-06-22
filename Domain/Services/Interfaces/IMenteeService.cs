using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface IMenteeService
    {
        public IEnumerable<MenteeViewModel> GetAll();
        public IEnumerable<MenteeViewModel> GetById(string menteeId);
        public int Insert(MenteeViewModel mentor);
        public int Update(MenteeViewModel mentor);
        public int ChangeStatus(string menteeId, bool status);
        public int Delete(string menteeId);
    }
}
