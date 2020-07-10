using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.ViewModels.EnrollModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/enrolls")]
    [ApiController]
    public class EnrollController : ControllerBase
    {

        #region Classes - Constructors
        protected readonly IEnrollService _enroll;

        public EnrollController(IEnrollService enroll)
        {
            _enroll = enroll;
        }
        #endregion



        /// <summary>
        /// Get list of enrolls. GET "api/enrolls"
        /// </summary>
        ///
        /// <returns>
        /// List containing enrolls. Message if list is empty.
        /// </returns>
        /// 
        /// <response code="200">Success</response>
        /// <response code="400">Not have enough infomation</response>
        /// <response code="401">Unauthorize</response>
        /// <response code="403">Forbidden from resource</response>
        /// <response code="500">Internal Error</response>
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
            ICollection<EnrollViewModel> result = null;

            try
            {
                result = _enroll.GetAll().ToList();
            }
            catch (Exception e)
            {
                return StatusCode((int)500, e);
            }

            if (result == null || result.Count == 0)
            {
                return Ok("There are no enrolls in the system");
            }
            return Ok(result);
        }



        /// <summary>
        /// Get enroll by Id.  GET "api/enrolls/{enrollId}"
        /// </summary>
        /// <param name="enrollId">
        /// The enroll's identifier.
        /// </param>
        /// <returns>
        /// Enroll with matching Id
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Enroll with matching Id not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{enrollId}")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 500
        public IActionResult GetById(string enrollId)
        {
            if (enrollId == null)
            {
                return BadRequest("User info must not be null");
            }

            ICollection<EnrollModel> result = null;

            try
            {
                result = _enroll.GetById(enrollId).ToList();
            }
            catch (Exception e)
            {
                return StatusCode((int)500, e);
            }

            if (result == null || result.Count == 0)
            {
                return NotFound("No enroll with ID " + enrollId + " was found.");
            }

            return Ok(result);
        }



        /// <summary>
        /// Insert an enroll into database. POST "api/enrolls".
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
        public IActionResult Insert(EnrollInsertModel enrollInsertModel)
        {
            if (enrollInsertModel == null)
            {
                return BadRequest("Enroll info must not be null");
            }

            int result = -1;

            try
            {
                result = _enroll.Insert(enrollInsertModel);
            }
            catch (Exception e)
            {
                StatusCode((int)500, e);
            }

            if (result == 0)
            {
                return BadRequest("Faulthy enroll info.");
            }
            if (result == 1)
            {
                return BadRequest("Already enrolled.");
            }

            return Ok("Enrolled into sharing session.");
        }



        /// <summary>
        /// Update an existing enroll. PUT "api/enrolls".
        /// </summary>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Enroll with matching Id not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        #region repCode 200 400 401 403 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
        public IActionResult Update(EnrollModel enrollModel)
        {
            if (enrollModel == null)
            {
                return BadRequest("Enroll info must not be null");
            }

            int result = -1;

            try
            {
                result = _enroll.Update(enrollModel);
            }
            catch (Exception e)
            {
                return StatusCode((int)500, e);
            }

            if (result == 0)
            {
                return BadRequest("Faulthy enroll info.");
            }
            if (result == 1)
            {
                return NotFound("Enroll not found");
            }

            return Ok("Updated enroll " + enrollModel.EnrollId);
        }



        /// <summary>
        /// Change status of an enroll (Disabled/Enabled). PUT "api/enrolls/status".
        /// </summary>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Enroll with matching Id not found</response>
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
        public IActionResult ChangeStatus(string enrollId, bool isDisable)
        {
            if (enrollId == null)
            {
                return BadRequest("EnrollId must not be null.");
            }

            int result = -1;

            try
            {
                result = _enroll.ChangeStatus(enrollId, isDisable);
            }
            catch (Exception e)
            {
                return StatusCode((int)500, e);
            }

            if (result == 0)
            {
                return BadRequest("Faulthy EnrollId.");
            }
            if (result == 1)
            {
                return NotFound("Enroll not found");
            }

            return isDisable ? Ok("Enroll is disabled.")
                : Ok("Enroll is enabled.");
        }



        /// <summary>
        /// Delete an enroll from database.. DELETE "api/enrolls/{enrollId}".
        /// </summary>
        /// <param name="enrollId">
        /// The enroll's identifier.
        /// </param>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Enroll with matching Id not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{enrollId}")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
        public IActionResult Delete(string enrollId)
        {
            if (enrollId == null)
            {
                return BadRequest("Enroll info must not be null");
            }

            int result = -1;

            try
            {
                result = _enroll.Delete(enrollId);
            }
            catch (Exception e)
            {
                return StatusCode((int)500, e);
            }

            if (result == 0)
            {
                return BadRequest("Faulthy enrollId.");
            }
            if (result == 1)
            {
                return NotFound("Enroll not found");
            }

            return Ok("Enroll is deleted.");
        }
    }
}
