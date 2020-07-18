using System;

namespace Domain.Models.SubscriptionModels
{
    public class SubscriptionViewModel
    {

        public Guid SubscriptionId { get; set; }
        public Guid MenteeId { get; set; }
        public Guid ChannelId { get; set; }
        public bool IsDisable { get; set; }
        public DateTime TimeSubscripted { get; set; }
    }
}
