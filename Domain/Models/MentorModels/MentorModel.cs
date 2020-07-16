using Domain.Models.ChannelModels;
using Domain.Models.UserModels;
using System;
using System.Collections.Generic;

namespace Domain.Models.MentorModels
{
    public class MentorModel
    {
        public Guid MentorId { get; set; }
        public UserViewModel User { get; set; }
        public ICollection<ChannelViewModel> Channels { get; set; }
        public bool IsDisable { get; set; }
    }
}
