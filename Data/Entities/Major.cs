using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Major
    {
        public Major()
        {
            Topic = new HashSet<Topic>();
        }

        public Guid MajorId { get; set; }
        public string MajorName { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsDisable { get; set; }

        public virtual ICollection<Topic> Topic { get; set; }
    }
}
