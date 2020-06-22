using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels
{
    public class SubscriptionViewModel
    {
        public Guid SubcriptionId { get; set; }
        public Guid MenteeId { get; set; }
        public Guid ChannelId { get; set; }
        public bool IsDisable { get; set; }
    }
}
