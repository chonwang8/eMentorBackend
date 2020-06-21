using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
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
        protected readonly IAuthService _auth;
        public AuthorizeController(IAuthService auth)
        {
            _auth = auth;
        }


        /// <summary>
        /// Register a user to database
        /// </summary>
        /// <returns>Registered the user</returns>
        /// <response code="200">Success</response>
        [HttpPost]
        public IActionResult Register(UserRegisterViewModel user)
        {
            string result = _auth.Register(user);
            return Ok(result);
        }


        /// <summary>
        /// Login
        /// </summary>
        /// <returns>User with matching Id</returns>
        /// <response code="200">Success</response>
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


        /// <summary>
        /// Login
        /// </summary>
        /// <returns>User with matching Id</returns>
        /// <response code="200">Success</response>
        [HttpPost]
        public IActionResult Login(AdminLoginViewModel adminLogin)
        {
            if (adminLogin == null)
            {
                return BadRequest("Bad Input");
            }
            string result = _auth.Login(adminLogin);
            if (result.Equals("Incorrect username or password"))
            {
                return NotFound("Incorrect username or password");
            }

            return Ok(result);
        }


        /// <summary>
        /// Login
        /// </summary>
        /// <returns>User with matching Id</returns>
        /// <response code="200">Success</response>
        [HttpPost]
        public IActionResult Login(UserLoginViewModel user)
        {
            string result = _auth.Login(user);
            return Ok(result);
        }

    }


}
