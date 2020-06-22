using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class CreateChannelDTO
    {
        public Guid TopicId { get; set; }
        public Guid MentorId { get; set; }
    }
}
