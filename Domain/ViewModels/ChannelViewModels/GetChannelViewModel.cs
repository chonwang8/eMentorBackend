using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels
{
    public class GetChannelViewModel
    {
        public Guid ChannelId { get; set; }
        public Guid TopicId { get; set; }
        public Guid MentorId { get; set; }
    }
}
