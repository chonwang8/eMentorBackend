using Domain.Models.ChannelModels;
using Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.MenteeModels
{
    public class MenteeEnrollCountModel
    {
        public Guid MenteeId { get; set; }
        public string MenteeEmail { get; set; }
        public int EnrollCount { get; set; }
    }
}
