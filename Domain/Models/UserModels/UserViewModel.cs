using System;

namespace Domain.Models.UserModels
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fullname { get; set; }
        public int YearOfBirth { get; set; }
        public string AvatarUrl { get; set; }
        public double? Balance { get; set; }
        public string Description { get; set; }
    }
}
