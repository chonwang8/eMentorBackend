using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface IUserService
    {
        public string Register(UserRegisterViewModel user);

        public string Login(UserLoginViewModel user);
    }
}
