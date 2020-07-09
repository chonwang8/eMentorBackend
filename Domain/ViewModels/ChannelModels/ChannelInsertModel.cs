using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels.ChannelModels
{
    public class ChannelInsertModel
    {
        public Guid TopicId { get; set; }
        public Guid MentorId { get; set; }
    }
}
