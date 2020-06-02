using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Channel
    {
        public Channel()
        {
            UserChannel = new HashSet<UserChannel>();
        }

        public Guid ChannelId { get; set; }
        public Guid UserId { get; set; }
        public bool IsDisable { get; set; }
        public Guid CreatedBy { get; set; }

        public virtual ICollection<UserChannel> UserChannel { get; set; }
    }
}
