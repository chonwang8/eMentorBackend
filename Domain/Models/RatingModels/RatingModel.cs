using Domain.Models.MentorModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.RatingModels
{
    public class RatingModel
    {
        public Guid MentorId { get; set; }
        public int? RatingCount { get; set; }
        public double? RatingScore { get; set; }

        public virtual MentorViewModel Mentor { get; set; }
    }
}
