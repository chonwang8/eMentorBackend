using Domain.Models.UserModels;
using System;

namespace Domain.Models.MentorModels
{
    public class MentorUpdateModel
    {
        public Guid MentorId { get; set; }
        public UserViewModel User { get; set; }
        public bool IsDisable { get; set; }
    }
}
