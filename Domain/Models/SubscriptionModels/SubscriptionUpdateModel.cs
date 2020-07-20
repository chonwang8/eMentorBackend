using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.SubscriptionModels
{
    public class SubscriptionUpdateModel
    {
        public Guid SubscriptionId { get; set; }
        public Guid MenteeId { get; set; }
        public Guid ChannelId { get; set; }
        public bool IsDisable { get; set; }
    }
}
