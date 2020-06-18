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




        public IEnumerable<UserViewModel> GetAll();
        public IEnumerable<UserViewModel> GetById(string userId);
        public int Insert(UserViewModel userInsertion);
        public int Update(UserViewModel user);
        public int Disable(UserStatusViewModel user);
        public int Delete(string userId);

    }
}
