using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Domain.Models.AdminModels
{
    public class AdminClaimsProfile
    {
        public AdminClaimsProfile() { }

        public AdminModel GetProfile(ClaimsIdentity identity)
        {
            AdminModel adminProfile = new AdminModel();
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
