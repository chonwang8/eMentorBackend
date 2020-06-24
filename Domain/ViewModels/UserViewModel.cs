using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels
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

    public class UserInsertViewModel
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fullname { get; set; }
        public int YearOfBirth { get; set; }
        public string AvatarUrl { get; set; }
    }

    public class UserLoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
    }

    public class UserRegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Fullname { get; set; }
        public int YearOfBirth { get; set; }
    }

    public class UserRoleViewModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }

    public class UserStatusViewModel
    {
        public Guid UserId { get; set; }
        public bool IsDisable { get; set; }
    }
}
