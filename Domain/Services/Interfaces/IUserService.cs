using Domain.DTO;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<UserViewModel> GetAll(GetAllDTO request);
        public IEnumerable<UserViewModel> GetById(string userId);
        public int Insert(UserViewModel userInsertion);
        public int Update(UserViewModel user);
        public int Disable(UserStatusViewModel user);
        public int Delete(string userId);

    }
}
