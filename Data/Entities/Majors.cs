using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Majors
    {
        public Majors()
        {
            Topics = new HashSet<Topics>();
        }

        public Guid MajorId { get; set; }
        public string MajorName { get; set; }
        public bool IsDisable { get; set; }
        public Guid CreatedBy { get; set; }

        public virtual ICollection<Topics> Topics { get; set; }
    }
}
