using System;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [EnableCors("MyPolicy")]
    [AllowAnonymous]
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
        /// <response code="400">Error</response>
        [HttpPost("register")]
        public IActionResult Register(UserRegisterViewModel user)
        {
            if (user == null)
            {
                return BadRequest("Bad Input");
            }

            string result = null;

            try
            {
                result = _auth.Register(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace + e.Source);
            }
            return Ok(result);
        }


        /// <summary>
        /// Login
        /// </summary>
        /// <returns>User with matching Id</returns>
        /// <response code="200">Success</response>
        [HttpPost("adminLogin")]
        public IActionResult Login(AdminLoginViewModel adminLogin)
        {
            if (adminLogin == null)
            {
                return BadRequest("Bad Input");
            }

            string result = null;

            try
            {
                result = _auth.Login(adminLogin);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace + e.Source);
            }

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
        [HttpPost("login")]
        public IActionResult Login(UserLoginViewModel user)
        {
            if (user == null)
            {
                return BadRequest("Bad Input");
            }

            string result = null;

            try
            {
                result = _auth.Login(user);
            }
            catch (Exception)
            {
                throw;
            }
            
            return Ok(result);
        }

    }


}
