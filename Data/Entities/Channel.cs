using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Channel
    {
        public Channel()
        {
            Sharing = new HashSet<Sharing>();
            Subcription = new HashSet<Subscription>();
        }

        public Guid ChannelId { get; set; }
        public Guid TopicId { get; set; }
        public Guid MentorId { get; set; }

        public virtual Mentor Mentor { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual ICollection<Sharing> Sharing { get; set; }
        public virtual ICollection<Subscription> Subcription { get; set; }
    }
}
