using Domain.Models.UserModels;
using System;

namespace Domain.Models.MentorModels
{
    public class MentorUpdateModel
    {
        public Guid MentorId { get; set; }
        public UserUpdateModel User { get; set; }
    }
}
