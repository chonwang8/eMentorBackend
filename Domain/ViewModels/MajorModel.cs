using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels
{
    public class MajorViewModel
    {
        public Guid MajorId { get; set; }
        public string MajorName { get; set; }
        public Guid CreatedBy { get; set; }
        public ICollection<TopicViewModel> Topic { get; set; }
    }
}
