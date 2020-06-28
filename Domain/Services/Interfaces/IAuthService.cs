using Domain.DTO;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface IAuthService
    {

        public string Register(UserRegisterViewModel user);
        public LoginResponseDTO Login(UserLoginViewModel user);
        public string Login(AdminLoginViewModel adminLogin);
    }
}
