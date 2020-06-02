using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Channels
    {
        public Channels()
        {
            UserChannels = new HashSet<UserChannels>();
        }

        public Guid ChannelId { get; set; }
        public Guid UserId { get; set; }
        public bool IsDisable { get; set; }
        public Guid CreatedBy { get; set; }

        public virtual ICollection<UserChannels> UserChannels { get; set; }
    }
}
