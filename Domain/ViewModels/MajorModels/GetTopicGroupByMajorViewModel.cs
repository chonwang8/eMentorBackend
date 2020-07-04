using System;
using System.Collections.Generic;

namespace Domain.ViewModels.MajorModels
{
    public class GetTopicGroupByMajorViewModel
    {
        public Guid MajorId { get; set; }

        public string MajorName { get; set; }

        public List<GetTopic> Topics { get; set; }

    }

    public class GetTopic
    {
        public Guid TopicId { get; set; }

        public string TopicName { get; set; }
    }
}
