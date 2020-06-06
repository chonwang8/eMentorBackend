using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface IMajorService
    {
        public List<GetTopicGroupByMajorViewModel> GetTopicGroupByMajor();
    }
}
