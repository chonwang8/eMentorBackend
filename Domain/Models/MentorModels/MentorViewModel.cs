using Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.MentorModels
{
    public class MentorViewModel
    {
        public Guid MentorId { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string AvatarUrl { get; set; }
        public string Description { get; set; }
    }
}
