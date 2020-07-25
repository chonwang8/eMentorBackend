using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.RatingModels
{
    public class RatingInsertModel
    {
        public Guid MentorId { get; set; }
        public int RatingScore { get; set; }
    }
}
