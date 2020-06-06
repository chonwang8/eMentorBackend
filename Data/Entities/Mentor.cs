using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Mentor
    {
        public Mentor()
        {
            Channel = new HashSet<Channel>();
        }

        public Guid MentorId { get; set; }
        public Guid UserId { get; set; }
        public bool IsDisable { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Channel> Channel { get; set; }
    }
}
