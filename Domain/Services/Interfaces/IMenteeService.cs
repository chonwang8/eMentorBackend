using Domain.DTO;
using Domain.ViewModels.MenteeModels;
using System.Collections.Generic;

namespace Domain.Services.Interfaces
{
    public interface IMenteeService
    {
        public IEnumerable<MenteeViewModel> GetAll(GetAllDTO request);
        public IEnumerable<MenteeViewModel> GetById(string menteeId);
        public int Insert(MenteeViewModel mentor);
        public int Update(MenteeViewModel mentor);
        public int ChangeStatus(string menteeId, bool status);
        public int Delete(string menteeId);
    }
}
