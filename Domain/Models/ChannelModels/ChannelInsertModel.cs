using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.ChannelModels
{
    public class ChannelInsertModel
    {
        public Guid TopicId { get; set; }
        public Guid MentorId { get; set; }
    }
}
