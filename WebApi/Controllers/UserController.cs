using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected readonly IUserService _user;
        public UserController(IUserService service)
        {
            _user = service;
        }

        /// <summary>
        /// View list of users
        /// </summary>
        /// <returns>List of users</returns>
        /// <response code="200">List of challenge</response>
        /// <response code="400">Not have enough infomation</response>
        /// <response code="401">Unauthorize</response>
        /// <response code="403">Forbidden from resource</response>
        /// <response code="404">Empty challenge list</response>
        /// <response code="500">Internal Error</response>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<UserViewModel> result = _user.GetAll().ToList();
            if (result == null || result.Count == 0)
            {
                return NotFound("There are no users in the system");
            }
            return Ok(result);
        }


        /// <summary>
        /// Return User with matching Id
        /// </summary>
        /// <returns>User with matching Id</returns>
        /// <response code="200">User found with matching Id</response>
        /// <response code="400">Invalid input</response>
        /// <response code="404">User with matching Id not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{userId}")]
        public IActionResult GetById(string userId)
        {
            if (userId == null)
            {
                return BadRequest("User info must not be null");
            }

            List<UserViewModel> result = _user.GetById(userId).ToList();

            if (result == null || result.Count == 0)
            {
                return NotFound("No user with userId " + userId + " found.");
            }

            return Ok(result);
        }


        /// <summary>
        /// Insert a user into database
        /// </summary>
        /// <returns>Query status</returns>
        /// <response code="200">User successfully inserted</response>
        /// <response code="400">Invalid input</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        public IActionResult Insert(UserViewModel user)
        {
            if (user == null)
            {
                return BadRequest("User info must not be null");
            }

            int result = _user.Insert(user);

            if (result == 0)
            {
                return BadRequest("Faulthy user info.");
            }
            if (result == 1)
            {
                return BadRequest("This user is already in the system");
            }

            return Ok("Insert user " + user.Email);
        }


        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <returns>Query status</returns>
        /// <response code="200">User successfully updated</response>
        /// <response code="400">Invalid input</response>
        /// <response code="404">User with matching Id not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        public IActionResult Update(UserViewModel user)
        {
            if (user == null)
            {
                return BadRequest("User info must not be null");
            }

            int result = _user.Insert(user);

            if (result == 0)
            {
                return BadRequest("Faulthy user info.");
            }
            if (result == 1)
            {
                return NotFound("User not found");
            }

            return Ok("Updated user " + user.Email);
        }


        /// <summary>
        /// Disable a user
        /// </summary>
        /// <returns>Query status</returns>
        /// <response code="200">Disabled user with matching Id</response>
        /// <response code="400">Invalid input</response>
        /// <response code="404">User with matching Id not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("status")]
        public IActionResult Disable(UserStatusViewModel user)
        {
            if (user == null)
            {
                return BadRequest("User info must not be null");
            }

            int result = _user.Disable(user);

            if (result == 0)
            {
                return BadRequest("Faulthy user info.");
            }
            if (result == 1)
            {
                return NotFound("User not found");
            }

            return Ok("User is disabled.");
        }


        /// <summary>
        /// Delete a user from database
        /// </summary>
        /// <returns>Query status</returns>
        /// <response code="200">User was deleted from database</response>
        /// <response code="400">Invalid input</response>
        /// <response code="404">User with matching Id not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        public IActionResult Delete(string userId)
        {
            if (userId == null)
            {
                return BadRequest("User info must not be null");
            }

            int result = _user.Delete(userId);

            if (result == 0)
            {
                return BadRequest("Faulthy user info.");
            }
            if (result == 1)
            {
                return NotFound("User not found");
            }

            return Ok("User is deleted.");
        }
    }



    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        protected readonly IUserService _service;
        public RegisterController(IUserService service)
        {
            _service = service;
        }


        /// <summary>
        /// Register a user to database
        /// </summary>
        /// <returns>Registered the user</returns>
        /// <response code="200">Success</response>
        [HttpPost]
        public IActionResult Register(UserRegisterViewModel user)
        {
            string result = _service.Register(user);
            return Ok(result);
        }
    }


    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        protected readonly IUserService _service;
        public LoginController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <returns>User with matching Id</returns>
        /// <response code="200">Success</response>
        [HttpPost]
        public IActionResult Login(UserLoginViewModel user)
        {
            string result = _service.Login(user);
            return Ok(result);
        }
    }
}
