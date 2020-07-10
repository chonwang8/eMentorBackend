using Domain.ViewModels.UserModels;
using System;

namespace Domain.ViewModels.MentorModels
{
    public class MentorUpdateModel
    {
        public Guid MentorId { get; set; }
        public UserViewModel User { get; set; }
        public bool IsDisable { get; set; }
    }
}
