using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels
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
