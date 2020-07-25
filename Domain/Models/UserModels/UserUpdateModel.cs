using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.UserModels
{
    public class UserUpdateModel
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fullname { get; set; }
        public int? YearOfBirth { get; set; }
        public string AvatarUrl { get; set; }
        public double? Balance { get; set; }
        public string Description { get; set; }
    }
}
