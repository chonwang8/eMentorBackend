using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class UserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Fullname { get; set; }
        public int YearOfBirth { get; set; }
        public bool IsMentor { get; set; }
        public string AvatarUrl { get; set; }
        public int Balance { get; set; }
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
