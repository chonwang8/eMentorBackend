using Domain.DTO;
using Domain.ViewModels.MentorModels;
using System.Collections.Generic;

namespace Domain.Services.Interfaces
{
    public interface IMentorService
    {
        public IEnumerable<MentorModel> GetAll(GetAllDTO request);
        public IEnumerable<MentorModel> GetById(string mentorId);
        public int Insert(MentorModel mentor);
        public int Update(MentorModel mentor);
        public int ChangeStatus(string mentorId, bool status);
        public int Delete(string mentorId);
    }
}
