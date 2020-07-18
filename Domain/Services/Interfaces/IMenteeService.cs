using Domain.DTO;
using Domain.Models.MenteeModels;
using System.Collections.Generic;

namespace Domain.Services.Interfaces
{
    public interface IMenteeService
    {
        public IEnumerable<MenteeModel> GetAll();
        public IEnumerable<MenteeModel> GetById(string menteeId);
        public int Insert(MenteeModel mentor);
        public int Update(MenteeModel mentor);
        public int ChangeStatus(string menteeId, bool status);
        public int Delete(string menteeId);
    }
}
