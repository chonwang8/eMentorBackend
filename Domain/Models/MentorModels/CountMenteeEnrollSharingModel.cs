using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.MentorModels
{
    public class CountMenteeSubcribeTopicModel
    {
        public Guid MentorId { get; set; }

        public int Counter { get; set; }
    }
}
