using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.TopicModels
{
    public class TopicEnrollCountModel
    {
        public Guid TopicId { get; set; }
        public string TopicName { get; set; }
        public int EnrollCount { get; set; }
    }
}
