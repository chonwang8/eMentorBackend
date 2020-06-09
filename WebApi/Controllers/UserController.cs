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


        [HttpGet]
        public IActionResult GetAll()
        {
            List<UserViewModel> result = _user.GetAll().ToList();
            if (result == null || result.Count == 0)
            {
                return Ok("There are no users in the system");
            }
            return Ok(result);
        }

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

        [HttpPost]
        public IActionResult Login(UserLoginViewModel user)
        {
            string result = _service.Login(user);
            return Ok(result);
        }
    }
}
