using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Topic
    {
        public Topic()
        {
            Sharing = new HashSet<Sharing>();
        }

        public Guid TopicId { get; set; }
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }
        public Guid MajorId { get; set; }
        public bool IsDisable { get; set; }
        public Guid CreatedBy { get; set; }

        public virtual Major Major { get; set; }
        public virtual ICollection<Sharing> Sharing { get; set; }
    }
}
