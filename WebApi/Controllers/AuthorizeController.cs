using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
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
        protected readonly IUserService _user;
        public AuthorizeController(IUserService user)
        {
            _user = user;
        }

        public IActionResult GoogleAuth()
        {
            string jwt = null;

            //  Decode Firebase Token to get User Info
            
            //var userProfile = claimInfo.GetProfile(HttpContext.User.Identity as ClaimsIdentity);

            //  Check Input

            //  Check for existing User in Database

            //  if (exist) => Create new user   => create Token

            //  else => create Token

            return Ok(jwt);
        }


    }


    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        protected readonly IAdminLogic _admin;
        public AdminController(IAdminLogic admin)
        {
            _admin = admin;
        }


        [HttpPost]
        public IActionResult Login(AdminLoginViewModel adminLogin)
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
    }
}
