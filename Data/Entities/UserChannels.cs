using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class UserChannels
    {
        public Guid UserChannelId { get; set; }
        public Guid UserId { get; set; }
        public Guid ChannelId { get; set; }
        public bool IsSubcriber { get; set; }

        public virtual Channels Channel { get; set; }
        public virtual Users User { get; set; }
    }
}
