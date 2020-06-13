using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class UpdateChannelDTO
    {
        public Guid ChannelId { get; set; }
        public Guid TopicId { get; set; }
        public Guid MentorId { get; set; }
    }
}
