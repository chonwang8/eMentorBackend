using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface IUserService
    {
        public string Register(UserRegisterModel user);

        public string Login(UserLoginModel user);
    }
}
