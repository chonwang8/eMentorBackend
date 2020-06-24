using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Channel
    {
        public Channel()
        {
            Sharing = new HashSet<Sharing>();
            Subscription = new HashSet<Subscription>();
        }

        public Guid ChannelId { get; set; }
        public Guid TopicId { get; set; }
        public Guid MentorId { get; set; }
        public bool IsDisable { get; set; }

        public virtual Mentor Mentor { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual ICollection<Sharing> Sharing { get; set; }
        public virtual ICollection<Subscription> Subscription { get; set; }
    }
}
