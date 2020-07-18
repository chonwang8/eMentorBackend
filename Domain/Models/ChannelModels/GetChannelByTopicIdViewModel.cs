using System;

namespace Domain.Models.ChannelModels
{
    public class GetChannelByTopicIdViewModel
    {
        public Guid ChannelId { get; set; }
        public Guid TopicId { get; set; }
        public string TopicName { get; set; }
        public Guid MentorId { get; set; }
        public string MentorName { get; set; }
    }
}
