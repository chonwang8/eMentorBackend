using Data.Entities;
using System;
using System.Collections.Generic;

namespace Domain.ViewModels.ChannelModels
{
    public class ChannelViewModel
    {
        public Guid ChannelId { get; set; }
        public string TopicName { get; set; }
        public string MentorName { get; set; }
        public virtual ICollection<Sharing> Sharing { get; set; }
        public virtual ICollection<Subscription> Subscription { get; set; }
    }
}
