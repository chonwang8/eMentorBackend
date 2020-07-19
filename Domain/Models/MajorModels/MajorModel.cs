using Domain.Models.TopicModels;
using System;
using System.Collections.Generic;

namespace Domain.Models.MajorModels
{
    public class MajorViewModel
    {
        public Guid MajorId { get; set; }
        public string MajorName { get; set; }
        public Guid CreatedBy { get; set; }
        public ICollection<TopicViewModel> Topic { get; set; }
    }
}
