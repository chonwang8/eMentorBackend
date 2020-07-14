using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.ViewModels.UserModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class UserController : ControllerBase
    {
        #region Classes - Constructors
        protected readonly IUserService _user;
        public UserController(IUserService service)
        {
            _user = service;
        }
        #endregion



        /// <summary>
        /// Get list of users. GET "api/users"
        /// </summary>
        /// 
        /// <returns>
        /// List containing users. Message if list is empty.
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        #region repCode 200 400 401 403 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 500
        public IActionResult GetAll()
        {
            List<UserViewModel> result = _user.GetAll().ToList();

            if (result == null || result.Count == 0)
            {
                return Ok("There are no users in the system");
            }
            return Ok(result);
        }



        /// <summary>
        /// Get user by Id. GET "api/users/{userId}"
        /// </summary>
        /// <param name="userId">
        /// The user's identifier.
        /// </param>
        /// <returns>
        /// User with matching Id
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">User with matching Id not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{userId}")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 500
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
        /// Insert a user into database. POST "api/users".
        /// </summary>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        #region repCode 200 400 401 403 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
        public IActionResult Insert(UserInsertViewModel userInsert)
        {
            if (userInsert == null)
            {
                return BadRequest("User info must not be null");
            }

            int result = _user.Insert(userInsert);

            if (result == 0)
            {
                return BadRequest("Faulthy user info.");
            }
            if (result == 1)
            {
                return BadRequest("This user is already in the system");
            }

            return Ok("Insert user " + userInsert.Email);
        }



        /// <summary>
        /// Update an existing user. PUT "api/users".
        /// </summary>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">User with matching Id not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        #region repCode 200 400 401 403 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
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
        /// Change status of a user (Disabled/Enabled). PUT "api/users/status".
        /// </summary>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">User with matching Id not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("status")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
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
        /// Delete a user from database. DELETE "api/users/{userId}".
        /// </summary>
        /// <param name="userId">
        /// The user's identifier.
        /// </param>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">User with matching Id not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{userId}")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
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
