using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Fullname { get; set; }
        public int YearOfBirth { get; set; }
        public string AvatarUrl { get; set; }
        public double? Balance { get; set; }
        public string Description { get; set; }
        public bool IsDisable { get; set; }
    }

    public class UserLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserRegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Fullname { get; set; }
        public int YearOfBirth { get; set; }
    }
}
