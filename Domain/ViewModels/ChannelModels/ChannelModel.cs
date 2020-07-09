using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels.ChannelModels
{
    public class ChannelModel
    {
        public Guid ChannelId { get; set; }
        public Guid TopicId { get; set; }
        public Guid MentorId { get; set; }
        public bool IsDisable { get; set; }
    }
}
