using Domain.DTO;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface IMajorService
    {
        public List<GetTopicGroupByMajorViewModel> GetTopicGroupByMajor();

        public List<GetMajorViewModel> GetAllMajor();

        public GetMajorViewModel GetMajorById(Guid MajorId);

        public bool DeleteMajorById(Guid MajorId);

        public bool UpdateMajorById(UpdateMajorDTO major);

        public bool CreateMajor(string MajorName);
    }
}
