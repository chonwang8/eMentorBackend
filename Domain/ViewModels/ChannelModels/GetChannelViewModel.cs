using System;

namespace Domain.ViewModels.ChannelModels
{
    public class GetChannelViewModel
    {
        public Guid ChannelId { get; set; }
        public Guid TopicId { get; set; }
        public Guid MentorId { get; set; }
    }
}
