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
    [Route("api/mentors")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class MentorController : ControllerBase
    {
        #region Classes - Constructors
        protected readonly IMentorService _mentor;
        public MentorController(IMentorService service)
        {
            _mentor = service;
        }
        #endregion



        /// <summary>
        /// Get list of mentors. GET "api/mentors"
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
        /// List containing mentors. Message if list is empty.
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

            List<MentorViewModel> result = _mentor.GetAll(paging).ToList();
            if (result == null || result.Count == 0)
            {
                return Ok("There are no users in the system");
            }
            return Ok(result);
        }



        /// <summary>
        /// Get mentor by Id. GET "api/mentors/{mentorId}"
        /// </summary>
        /// <param name="mentorId">
        /// The user's identifier.
        /// </param>
        /// <returns>
        /// User with matching Id
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Mentor with matching Id not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{mentorId}")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 500
        public IActionResult GetById(string mentorId)
        {
            if (mentorId == null)
            {
                return BadRequest("Mentor info must not be null");
            }

            List<MentorViewModel> result = _mentor.GetById(mentorId).ToList();

            if (result == null || result.Count == 0)
            {
                return NotFound("No mentor with mentorId " + mentorId + " found.");
            }

            return Ok(result);
        }



        /// <summary>
        /// Insert a mentor into database. POST "api/mentors".
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
        public IActionResult Insert(MentorViewModel mentor)
        {
            if (mentor == null)
            {
                return BadRequest("User info must not be null");
            }

            int result = _mentor.Insert(mentor);

            if (result == 0)
            {
                return BadRequest("Faulthy mentor info.");
            }
            if (result == 1)
            {
                return BadRequest("This user is already a mentor");
            }

            return Ok("Mentor Inserted");
        }



        /// <summary>
        /// Update an existing mentor. PUT "api/mentors".
        /// </summary>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Mentor with matching Id not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        #region repCode 200 400 401 403 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
        public IActionResult Update(MentorViewModel mentor)
        {
            if (mentor == null)
            {
                return BadRequest("Mentor info must not be null");
            }

            int result = _mentor.Insert(mentor);

            if (result == 0)
            {
                return BadRequest("Faulthy mentor info.");
            }
            if (result == 1)
            {
                return NotFound("Mentor not found");
            }

            return Ok("Mentor updated");
        }



        /// <summary>
        /// Change status of a mentor (Disabled/Enabled). PUT "api/mentors/status".
        /// </summary>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Mentor with matching Id not found</response>
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
        public IActionResult ChangeStatus(string mentorId, bool isDisable)
        {
            if (mentorId == null)
            {
                return BadRequest("MentorId must not be null.");
            }

            int result = _mentor.ChangeStatus(mentorId, isDisable);

            if (result == 0)
            {
                return BadRequest("Faulthy mentor info.");
            }
            if (result == 1)
            {
                return NotFound("Mentor not found");
            }

            return isDisable ? Ok("Mentor is disabled.")
                : Ok("Mentor is enabled.");
        }



        /// <summary>
        /// Delete a mentor from database. DELETE "api/mentors/{mentorId}".
        /// </summary>
        /// <param name="mentorId">
        /// The mentor's identifier.
        /// </param>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Mentor with matching Id not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{mentorId}")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
        public IActionResult Delete(string mentorId)
        {
            if (mentorId == null)
            {
                return BadRequest("Mentor info must not be null");
            }

            int result = _mentor.Delete(mentorId);

            if (result == 0)
            {
                return BadRequest("Faulthy mentor info.");
            }
            if (result == 1)
            {
                return NotFound("Mentor not found");
            }

            return Ok("Mentor is deleted.");
        }



    }
}
