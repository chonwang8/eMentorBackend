using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Mentee
    {
        public Mentee()
        {
            Subcription = new HashSet<Subscription>();
        }

        public Guid MenteeId { get; set; }
        public Guid UserId { get; set; }
        public bool IsDisable { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Subscription> Subcription { get; set; }
    }
}
