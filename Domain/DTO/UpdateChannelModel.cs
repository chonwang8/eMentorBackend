using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class UpdateChannelModel
    {
        public Guid ChannelId { get; set; }
        public Guid TopicId { get; set; }
        public Guid MentorId { get; set; }
    }
}
