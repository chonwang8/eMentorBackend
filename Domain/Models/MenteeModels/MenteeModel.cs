using Domain.Models.ChannelModels;
using Domain.Models.SubscriptionModels;
using Domain.Models.UserModels;
using System;
using System.Collections.Generic;

namespace Domain.Models.MenteeModels
{
    public class MenteeModel
    {
        public Guid MenteeId { get; set; }
        public UserViewModel User { get; set; }
        public ICollection<SubscriptionViewModel> Subscription { get; set; }
        public bool IsDisable { get; set; }
    }
}
