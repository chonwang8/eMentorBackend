using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class MajorModel
    {
        public Guid MajorId { get; set; }
        public string MajorName { get; set; }
        public Guid CreatedBy { get; set; }
        public ICollection<TopicModel> Topic { get; set; }
    }
}
