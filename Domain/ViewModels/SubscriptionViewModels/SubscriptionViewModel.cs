using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels.SubscriptionViewModels
{
    public class SubscriptionViewModel
    {
        public Guid SubcriptionId { get; set; }
        public Guid MenteeId { get; set; }
        public Guid ChannelId { get; set; }
    }
}
