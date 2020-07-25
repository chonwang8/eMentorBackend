using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.TopicModels
{
    public class CountMenteeSubcribeTopicModel
    {
        public Guid TopicId { get; set; }

        public int Counter { get; set; }
    }
}
