using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using Domain.Helper.AdminFunctions.Interfaces;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [Authorize]
    public class AuthorizeController : ControllerBase
    {
        protected readonly IUserService _user;
        public AuthorizeController(IUserService user)
        {
            _user = user;
        }

        [HttpPost]
        public IActionResult GoogleAuth()
        {
            //if (adminLogin == null)
            //{
            //    return BadRequest("Bad Input");
            //}
            //string result = _admin.Login(adminLogin);
            //if (result.Equals("Incorrect username or password"))
            //{
            //    return NotFound("Incorrect username or password");
            //}

            return Ok("Auth success");
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
