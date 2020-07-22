using Domain.Models.ChannelModels;
using Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.MenteeModels
{
    public class MenteeSubbedChannelModel
    {
        public Guid MenteeId { get; set; }
        public UserViewModel User { get; set; }
        public ICollection<ChannelViewModel> Channels { get; set; }
    }
}
