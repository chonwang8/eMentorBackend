using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Domain.ViewModels
{
    public class AdminViewModel
    {
        public Guid AdminId { get; set; }
        public string AdminUsername { get; set; }
    }

    public class AdminLoginViewModel
    {
        public string AdminUsername { get; set; }
        public string Password { get; set; }
    }

    public class AdminClaimsProfile
    {

        public AdminClaimsProfile() { }

        public AdminViewModel GetProfile(ClaimsIdentity identity)
        {
            AdminViewModel adminProfile = new AdminViewModel();
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                string adminId = claims.FirstOrDefault(a => a.Type == "admin_id").Value;
                adminProfile.AdminId = new Guid(adminId);
                adminProfile.AdminUsername = claims.FirstOrDefault(a => a.Type == "admin_username").Value;
            }
            return adminProfile;
        }
    }
}
