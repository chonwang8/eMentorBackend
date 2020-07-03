using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    [EnableCors("MyPolicy")]
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
        public IActionResult GetAll(string size, string index, string asc)
        {
            int pageSize, pageIndex;
            bool IsAscended = false;
            GetAllDTO paging = null;

            #region Set default paging values if null or empty input

            if (!string.IsNullOrWhiteSpace(size))
            {
                if (!size.All(char.IsDigit))
                {
                    return BadRequest("Invalid paging values");
                }
                pageSize = int.Parse(size);
            }
            else
            {
                pageSize = 40;
            }

            if (!string.IsNullOrWhiteSpace(index))
            {
                if (!index.All(char.IsDigit))
                {
                    return BadRequest("Invalid paging values");
                }
                pageIndex = int.Parse(index);
            }
            else
            {
                pageIndex = 1;
            }

            if (!string.IsNullOrWhiteSpace(asc))
            {
                if (!asc.ToLower().Equals("true") || !asc.ToLower().Equals("false"))
                {
                    return BadRequest("Invalid paging values");
                }
                IsAscended = bool.Parse(asc);
            }

            #endregion

            paging = new GetAllDTO
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                IsAscending = false
            };

            List<UserViewModel> result = _user.GetAll(paging).ToList();

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
        public IActionResult Insert(UserInsertViewModel userInsert)
        {
            UserInsertViewModel user = null;
            try
            {
                user = (UserInsertViewModel) userInsert;
            } catch (Exception)
            {
                return BadRequest("Request body does not fit UserInsertViewModel parameters");
            }

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

            int result = _user.Update(user);

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
        public IActionResult ChangeStatus(string userId, bool isDisable)
        {
            if (userId == null)
            {
                return BadRequest("UserId must not be null.");
            }

            int result = _user.ChangeStatus(userId, isDisable);

            if (result == 0)
            {
                return BadRequest("Faulthy UserId.");
            }
            if (result == 1)
            {
                return NotFound("User not found");
            }

            return isDisable ? Ok("User is disabled.")
                : Ok("User is enabled.");
        }


        /// <summary>
        /// Delete a user from database
        /// </summary>
        /// <returns>Query status</returns>
        /// <response code="200">User was deleted from database</response>
        /// <response code="400">Invalid input</response>
        /// <response code="404">User with matching Id not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{userId}")]
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
}
