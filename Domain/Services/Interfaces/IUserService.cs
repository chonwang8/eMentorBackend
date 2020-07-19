using Domain.DTO;
using Domain.Models.UserModels;
using System.Collections.Generic;

namespace Domain.Services.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<UserViewModel> GetAll();
        public IEnumerable<UserViewModel> GetById(string userId);
        public int Insert(UserInsertModel userInsert);
        public int Update(UserViewModel user);
        public int ChangeStatus(string subscriptionId, bool status);
        public int Delete(string userId);

    }
}
