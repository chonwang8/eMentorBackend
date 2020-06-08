using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels
{
    public class TopicViewModel
    {
        public Guid TopicId { get; set; }
        public string TopicName { get; set; }
        public Guid MajorId { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
