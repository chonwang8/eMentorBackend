using Domain.Models.SubscriptionModels;
using Domain.Models.UserModels;
using System;
using System.Collections.Generic;

namespace Domain.Models.MenteeModels
{
    public class MenteeViewModel
    {
        public Guid MenteeId { get; set; }
        public virtual UserViewModel User { get; set; }
        public virtual ICollection<SubscriptionViewModel> Subscription { get; set; }
    }
}
