using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Helper.AdminFunctions.Interfaces
{
    public interface IAdminLogic
    {
        public string Login(AdminLoginViewModel adminLogin);
    }
}
