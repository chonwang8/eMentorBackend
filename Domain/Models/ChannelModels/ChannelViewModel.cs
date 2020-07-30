using System;

namespace Domain.Models.ChannelModels
{
    public class ChannelViewModel
    {
        public Guid ChannelId { get; set; }
        public string TopicName { get; set; }
        public string MentorName { get; set; }
        public bool IsDisable { get; set; }
    }
}
