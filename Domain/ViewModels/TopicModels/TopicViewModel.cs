using Domain.ViewModels.ChannelModels;
using System;
using System.Collections.Generic;

namespace Domain.ViewModels.TopicModels
{
    public class TopicViewModel
    {
        public Guid TopicId { get; set; }
        public string TopicName { get; set; }
        public Guid MajorId { get; set; }
        public Guid CreatedBy { get; set; }


        public virtual ICollection<ChannelViewModel> ChannelList { get; set; }
    }
}
