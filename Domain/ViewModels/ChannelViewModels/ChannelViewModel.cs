using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels
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
