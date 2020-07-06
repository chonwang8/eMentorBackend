using System;

namespace Domain.ViewModels.ChannelModels
{
    public class GetChannelViewModel
    {
        public Guid ChannelId { get; set; }
        public string TopicName { get; set; }
        public string MentorName { get; set; }
    }
}
