using System;

namespace Domain.ViewModels.SubscriptionModels
{
    public class SubscriptionModel
    {
        public Guid SubcriptionId { get; set; }
        public Guid MenteeId { get; set; }
        public Guid ChannelId { get; set; }
        public bool IsDisable { get; set; }
    }
}
