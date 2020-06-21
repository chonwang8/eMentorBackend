using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Subscription
    {
        public Subscription()
        {
            Enroll = new HashSet<Enroll>();
        }

        public Guid SubcriptionId { get; set; }
        public Guid MenteeId { get; set; }
        public Guid ChannelId { get; set; }
        public DateTime TimeSubscripted { get; set; }
        public bool IsDisable { get; set; }

        public virtual Channel Channel { get; set; }
        public virtual Mentee Mentee { get; set; }
        public virtual ICollection<Enroll> Enroll { get; set; }
    }
}
