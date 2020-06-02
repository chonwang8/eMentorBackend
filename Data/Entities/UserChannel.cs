using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class UserChannel
    {
        public Guid UserChannelId { get; set; }
        public Guid UserId { get; set; }
        public Guid ChannelId { get; set; }
        public bool IsSubcriber { get; set; }

        public virtual Channel Channel { get; set; }
        public virtual User User { get; set; }
    }
}
