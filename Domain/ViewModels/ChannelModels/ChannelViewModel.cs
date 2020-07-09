using System;

namespace Domain.ViewModels.ChannelModels
{
    public class ChannelViewModel
    {
        public Guid ChannelId { get; set; }
        public string TopicName { get; set; }
        public string MentorName { get; set; }
    }
}
