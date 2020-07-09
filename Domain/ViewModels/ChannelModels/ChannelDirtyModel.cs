using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels.ChannelModels
{
    public class ChannelDirtyModel
    {
        public Guid ChannelId { get; set; }
        public string TopicName { get; set; }
        public string MentorName { get; set; }


        //  This is why it's called dirty. ICollection still use Entity class.
        //  I will fix this later, when i still have time.
        public virtual ICollection<Sharing> Sharing { get; set; }
        public virtual ICollection<Subscription> Subscription { get; set; }
    }
}
