using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.SubscriptionModels
{
    public class SubscriptionInsertModel
    {
        public Guid MenteeId { get; set; }
        public Guid ChannelId { get; set; }
        public bool IsDisable { get; set; }
    }
}
