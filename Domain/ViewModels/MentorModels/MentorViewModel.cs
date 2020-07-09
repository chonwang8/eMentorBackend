using Domain.ViewModels.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels.MentorModels
{
    public class MentorViewModel
    {
        public Guid MentorId { get; set; }
        public UserViewModel User { get; set; }
    }
}
