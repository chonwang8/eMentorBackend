using Data.Entities;
using Domain.ViewModels.SubscriptionViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels.ChannelViewModels
{
    public class ChannelSubsListViewModel
    {
        public Guid ChannelId { get; set; }
        public Guid TopicId { get; set; }
        public string TopicName { get; set; }
        public Guid MentorId { get; set; }
        public string MentorName { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
