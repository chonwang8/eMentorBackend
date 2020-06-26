using Domain.DTO;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface IMentorService
    {
        public IEnumerable<MentorViewModel> GetAll(GetAllDTO request);
        public IEnumerable<MentorViewModel> GetById(string mentorId);
        public int Insert(MentorViewModel mentor);
        public int Update(MentorViewModel mentor);
        public int ChangeStatus(string mentorId, bool status);
        public int Delete(string mentorId);
    }
}
