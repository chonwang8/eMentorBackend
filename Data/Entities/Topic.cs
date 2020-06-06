using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Topic
    {
        public Topic()
        {
            Channel = new HashSet<Channel>();
        }

        public Guid TopicId { get; set; }
        public string TopicName { get; set; }
        public Guid MajorId { get; set; }
        public Guid CreatedBy { get; set; }

        public virtual Major Major { get; set; }
        public virtual ICollection<Channel> Channel { get; set; }
    }
}
