using Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.MenteeModels
{
    public class MenteeUpdateModel
    {
        public Guid MenteeId { get; set; }
        public UserUpdateModel User { get; set; }
    }
}
