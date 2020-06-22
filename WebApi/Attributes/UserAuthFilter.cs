using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Attributes
{
    public class UserAuthFilter : Attribute, IAuthorizationFilter
    {
        private readonly string _permission;

        public UserAuthFilter(string permission)
        {
            _permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var uid = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id").Value;
                var email = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_email").Value;
                var role = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_role").Value;
                if (!_permission.Contains(role))
                {
                    context.Result = new ForbidResult();
                }
            }
            catch (NullReferenceException)
            {
                context.Result = new ForbidResult("Invalid Operation : no token");
            }
        }
    }
}
