using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Helper.AdminFunctions.Interfaces;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        protected readonly IAdminLogic _admin;
        protected readonly IUserService _user;

        public AuthorizeController(IAdminLogic admin, IUserService user)
        {
            _admin = admin;
            _user = user;
        }

        [HttpPost]
        public IActionResult AdminLogin(AdminLoginViewModel adminLogin)
        {
            if (adminLogin == null)
            {
                return BadRequest("Bad Input");
            }
            string result = _admin.Login(adminLogin);
            if (result.Equals("Incorrect username or password"))
            {
                return NotFound("Incorrect username or password");
            }

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetToken()
        {
            AdminClaimsProfile claimProfile = new AdminClaimsProfile();
            AdminViewModel admin = claimProfile.GetProfile(HttpContext.User.Identity as ClaimsIdentity);
            return Ok(admin);
        }

    }
}
