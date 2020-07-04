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
    [Route("api/mentees")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class MenteeController : ControllerBase
    {
        #region Classes - Constructors
        protected readonly IMenteeService _mentee;
        public MenteeController(IMenteeService service)
        {
            _mentee = service;
        }
        #endregion



        /// <summary>
        /// Get list of mentees. GET "api/mentees"
        /// </summary>
        /// 
        /// <param name="size">
        /// The number of items on a page. If null will be 40 by default.
        /// </param>
        /// <param name="index">
        /// The page number where paging is started. If null will be 1 by default.
        /// </param>
        /// <param name="asc">
        /// Boolean value determining whether return list will be null or not. If null will be false by default.
        /// </param>
        /// 
        /// <returns>
        /// List containing mentees. Message if list is empty.
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

            List<MenteeViewModel> result = _mentee.GetAll(paging).ToList();
            if (result == null || result.Count == 0)
            {
                return Ok("There are no mentees in the system");
            }
            return Ok(result);
        }



        /// <summary>
        /// Get mentee by Id. GET "api/mentees/{menteeId}"
        /// </summary>
        /// <param name="menteeId">
        /// The user's identifier.
        /// </param>
        /// <returns>
        /// User with matching Id
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Mentee with matching Id not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{menteeId}")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 500
        public IActionResult GetById(string menteeId)
        {
            if (menteeId == null)
            {
                return BadRequest("Mentee info must not be null");
            }

            List<MenteeViewModel> result = _mentee.GetById(menteeId).ToList();

            if (result == null || result.Count == 0)
            {
                return NotFound("No mentee with Id " + menteeId + " found.");
            }

            return Ok(result);
        }



        /// <summary>
        /// Insert a mentee into database. POST "api/mentees".
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
        public IActionResult Insert(MenteeViewModel mentee)
        {
            if (mentee == null)
            {
                return BadRequest("Mentee info must not be null");
            }

            int result = _mentee.Insert(mentee);

            if (result == 0)
            {
                return BadRequest("Faulthy mentee info.");
            }
            if (result == 1)
            {
                return BadRequest("This user is already a mentee");
            }

            return Ok("Mentee Inserted");
        }



        /// <summary>
        /// Update an existing mentee. PUT "api/mentees".
        /// </summary>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Mentee with matching Id not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        #region repCode 200 400 401 403 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
        public IActionResult Update(MenteeViewModel mentee)
        {
            if (mentee == null)
            {
                return BadRequest("Mentee info must not be null");
            }

            int result = _mentee.Insert(mentee);

            if (result == 0)
            {
                return BadRequest("Faulthy mentee info.");
            }
            if (result == 1)
            {
                return NotFound("Mentee not found");
            }

            return Ok("Mentee updated");
        }



        /// <summary>
        /// Change status of a mentee (Disabled/Enabled). PUT "api/mentees/status".
        /// </summary>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Mentee with matching Id not found</response>
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
        public IActionResult ChangeStatus(string menteeId, bool isDisable)
        {
            if (menteeId == null)
            {
                return BadRequest("MenteeId must not be null.");
            }

            int result = _mentee.ChangeStatus(menteeId, isDisable);

            if (result == 0)
            {
                return BadRequest("Faulthy mentee info.");
            }
            if (result == 1)
            {
                return NotFound("Mentee not found");
            }

            return isDisable ? Ok("Mentee is disabled.")
                : Ok("Mentee is enabled.");
        }



        /// <summary>
        /// Delete a mentee from database. DELETE "api/mentees/{menteeId}".
        /// </summary>
        /// <param name="menteeId">
        /// The mentee's identifier.
        /// </param>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Mentee with matching Id not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{menteeId}")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
        public IActionResult Delete(string menteeId)
        {
            if (menteeId == null)
            {
                return BadRequest("Mentee info must not be null");
            }

            int result = _mentee.Delete(menteeId);

            if (result == 0)
            {
                return BadRequest("Faulthy mentee info.");
            }
            if (result == 1)
            {
                return NotFound("Mentee not found");
            }

            return Ok("Mentee is deleted.");
        }

    }
}
