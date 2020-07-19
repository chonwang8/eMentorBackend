using Domain.Models.SubscriptionModels;
using Domain.Models.UserModels;
using System;
using System.Collections.Generic;

namespace Domain.Models.MenteeModels
{
    public class MenteeViewModel
    {
        public Guid MenteeId { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string AvatarUrl { get; set; }
        public string Description { get; set; }
    }
}
