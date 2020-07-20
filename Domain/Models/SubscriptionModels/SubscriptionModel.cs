using Domain.Models.ChannelModels;
using Domain.Models.EnrollModels;
using Domain.Models.MenteeModels;
using System;
using System.Collections.Generic;

namespace Domain.Models.SubscriptionModels
{
    public class SubscriptionModel
    {
        public Guid SubscriptionId { get; set; }
        public MenteeViewModel Mentee { get; set; }
        public ChannelViewModel Channel { get; set; }
        public bool IsDisable { get; set; }
        public DateTime TimeSubscripted { get; set; }
        public virtual ICollection<EnrollViewModel> Enroll { get; set; }
    }
}
