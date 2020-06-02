using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Topics
    {
        public Topics()
        {
            Sharings = new HashSet<Sharings>();
        }

        public Guid TopicId { get; set; }
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }
        public Guid MajorId { get; set; }
        public bool IsDisable { get; set; }
        public Guid CreatedBy { get; set; }

        public virtual Majors Major { get; set; }
        public virtual ICollection<Sharings> Sharings { get; set; }
    }
}
