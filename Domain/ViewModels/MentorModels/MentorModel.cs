using Domain.ViewModels.ChannelModels;
using Domain.ViewModels.UserModels;
using System;
using System.Collections.Generic;

namespace Domain.ViewModels.MentorModels
{
    public class MentorModel
    {
        public Guid MentorId { get; set; }
        public UserViewModel User { get; set; }
        public bool IsDisable { get; set; }
        public ICollection<ChannelViewModel> Channels { get; set; }
    }
}
