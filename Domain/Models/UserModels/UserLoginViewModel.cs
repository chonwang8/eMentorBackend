using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.UserModels
{
    public class UserLoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
    }
}
