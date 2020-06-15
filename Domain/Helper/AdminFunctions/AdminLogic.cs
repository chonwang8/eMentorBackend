using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.Helper.AdminFunctions.Interfaces;
using Domain.Helper.DataObjects;
using Domain.ViewModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Helper.AdminFunctions
{
    public class AdminLogic : IAdminLogic
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IOptions<AppSetting> _options;
        protected TokenManager tokenManager;

        public AdminLogic(IUnitOfWork uow, IOptions<AppSetting> options)
        {
            _uow = uow;
            _options = options;
            tokenManager = new TokenManager(_options);
        }


        public string Login(AdminLoginViewModel adminLogin)
        {
            Admin loggedAdmin = _uow
                .GetRepository<Admin>()
                .GetAll()
                .SingleOrDefault(a => a.AdminUsername == adminLogin.AdminUsername);
            if (loggedAdmin != null)
            {
                if (loggedAdmin.Password != adminLogin.Password)
                    return "Incorrect username or password";
            }

            string jwtToken = tokenManager.CreateAccessToken(new AdminViewModel
            {
                AdminId = loggedAdmin.AdminId,
                AdminUsername = loggedAdmin.AdminUsername
            });

            return jwtToken;
        }
    }
}
