using Domain.ViewModels.SubscriptionModels;
using Domain.ViewModels.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels.MenteeModels
{
    public class MenteeViewModel
    {
        public Guid MenteeId { get; set; }
        public virtual UserViewModel User { get; set; }
        public virtual ICollection<SubscriptionViewModel> Subscription { get; set; }
    }
}
