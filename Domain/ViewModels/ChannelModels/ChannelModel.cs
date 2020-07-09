using Domain.ViewModels.MentorModels;
using Domain.ViewModels.SharingModels;
using Domain.ViewModels.SubscriptionModels;
using Domain.ViewModels.TopicModels;
using System;
using System.Collections.Generic;

namespace Domain.ViewModels.ChannelModels
{
    public class ChannelModel
    {
        public Guid ChannelId { get; set; }
        public TopicViewModel Topic { get; set; }
        public MentorViewModel Mentor { get; set; }
        public virtual ICollection<SharingViewModel> Sharing { get; set; }
        public virtual ICollection<SubscriptionViewModel> Subscription { get; set; }
    }
}
