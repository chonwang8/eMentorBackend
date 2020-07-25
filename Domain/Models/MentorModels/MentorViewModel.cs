using Data.Entities;
using Domain.Models.ChannelModels;
using Domain.Models.RatingModels;
using Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.MentorModels
{
    public class MentorViewModel
    {
        public Guid MentorId { get; set; }
        //  Supposed to be UserViewModel, but fuck it
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string AvatarUrl { get; set; }
        public string Description { get; set; }
        //
        public RatingViewModel Rating { get; set; }
        public virtual ICollection<ChannelViewModel> Channels { get; set; }
    }
}
