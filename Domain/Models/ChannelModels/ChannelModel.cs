using Domain.Models.MentorModels;
using Domain.Models.SharingModels;
using Domain.Models.SubscriptionModels;
using Domain.Models.TopicModels;
using System;
using System.Collections.Generic;

namespace Domain.Models.ChannelModels
{
    public class ChannelModel
    {
        public Guid ChannelId { get; set; }
        public TopicViewModel Topic { get; set; }
        public MentorViewModel Mentor { get; set; }
        public virtual ICollection<SharingViewModel> Sharing { get; set; }
        public virtual ICollection<SubscriptionViewModel> Subscription { get; set; }
        public bool IsDisable { get; set; }
    }
}
