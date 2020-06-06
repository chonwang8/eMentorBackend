using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class TopicModel
    {
        public Guid TopicId { get; set; }
        public string TopicName { get; set; }
        public Guid MajorId { get; set; }
        public Guid CreatedBy { get; set; }

        public virtual ICollection<ChannelModel> Channel { get; set; }
    }
}
