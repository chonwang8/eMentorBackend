using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Rating
    {
        public Guid MentorId { get; set; }
        public double? RatingScore { get; set; }
        public int? RatingCount { get; set; }

        public virtual Mentor Mentor { get; set; }
    }
}
