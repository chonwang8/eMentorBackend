using Domain.DTO.AuthDTOs;
using Domain.Services.Interfaces;
using Domain.ViewModels.AdminModels;
using Domain.ViewModels.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [EnableCors("MyPolicy")]
    [AllowAnonymous]
    public class AuthorizeController : ControllerBase
    {
        #region Classes - Constructors
        protected readonly IAuthService _auth;
        public AuthorizeController(IAuthService auth)
        {
            _auth = auth;
        }
        #endregion


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
        [HttpPost("login")]
        public IActionResult Login(UserLoginViewModel userModel)
        {
            #region Check Input
            if (userModel == null)
            {
                return BadRequest("Bad Input");
            }
            #endregion

            LoginResponseDTO result = null;

            try
            {
                result = _auth.Login(userModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            return Ok(result);
        }


        /// <summary>
        /// Login
        /// </summary>
        /// <returns>User with matching Id</returns>
        /// <response code="200">Success</response>
        [HttpPost("adminLogin")]
        public IActionResult Login(AdminLoginModel adminLogin)
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

            if (result == null)
            {
                return NotFound("Incorrect username or password");
            }

            return Ok("Login Successfully.\n\n\n " + result);
        }


    }

}
