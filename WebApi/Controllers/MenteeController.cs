using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.Models.MenteeModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Domain.DTO.ResponseDtos;

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
        /// Get list of mentees.
        /// </summary>
        /// 
        /// <returns>
        /// List containing mentees. Message if list is empty.
        /// </returns>
        /// 
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
            BaseResponseDto<MenteeViewModel> responseDto = null;
            ICollection<MenteeViewModel> result = null;

            try
            {
                responseDto = _mentee.GetAll();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            if (responseDto.Status == 1 || responseDto.Status == 2)
            {
                return Ok(responseDto.Message);
            }

            result = responseDto.Content.ToList();

            return Ok(result);
        }



        /// <summary>
        /// Get mentee by Id.
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
            BaseResponseDto<MenteeModel> responseDto = null;
            ICollection<MenteeModel> result = null;

            if (menteeId == null)
            {
                return BadRequest("Mentee Id must not be null");
            }

            try
            {
                responseDto = _mentee.GetById(menteeId);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }


            if (responseDto.Status == 1)
            {
                return BadRequest(responseDto.Message);
            }

            if (responseDto.Status == 2)
            {
                return Ok(responseDto.Message);
            }

            //  finalize
            result = responseDto.Content.ToList();
            return Ok(result);
        }



        /// <summary>
        /// Generate a JWT for mentee.
        /// </summary>
        /// <param name="email">
        /// The mentor's identifier.
        /// </param>
        /// <returns>
        /// Mentor with matching Id along with UserInfo
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Mentor with matching Id not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("auth/{email}")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 500
        public IActionResult GoogleLogin(string email)
        {
            BaseResponseDto responseDto = null;

            if (email == null)
            {
                return BadRequest("Email must not be null");
            }

            try
            {
                responseDto = _mentee.GoogleLogin(email);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }


            if (responseDto.Status == 1)
            {
                return BadRequest(responseDto.Message);
            }


            return Ok(responseDto.Message);
        }



        /// <summary>
        /// Insert a mentee into database.
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
        public IActionResult Insert(MenteeInsertModel menteeInsertModel)
        {
            BaseResponseDto responseDto = null;

            if (menteeInsertModel == null)
            {
                return BadRequest("Mentee info must not be null");
            }

            try
            {
                responseDto = _mentee.Insert(menteeInsertModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            return Ok(responseDto.Message);
        }



        /// <summary>
        /// Update an existing mentee.
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
        public IActionResult Update(MenteeUpdateModel menteeUpdateModel)
        {
            BaseResponseDto responseDto = null;

            if (menteeUpdateModel == null)
            {
                return BadRequest("Mentee info must not be null");
            }

            try
            {
                responseDto = _mentee.Update(menteeUpdateModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message + "  \n  \n  \n  " + e.StackTrace);
            }

            return Ok(responseDto.Message);
        }



        /// <summary>
        /// Change status of a mentee (Disabled/Enabled).
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
        public IActionResult ChangeStatus(string menteeId, bool? isDisable)
        {
            BaseResponseDto responseDto = null;

            if (menteeId == null)
            {
                return BadRequest("Mentee Id must not be null.");
            }
            if (isDisable.Equals(null))
            {
                return BadRequest("Must specify isDisable parameter in order to allow this function works correctly");
            }

            bool disable = isDisable.Value;

            try
            {
                responseDto = _mentee.ChangeStatus(menteeId, disable);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            if (responseDto.Status == 1 || responseDto.Status == 2)
            {
                return BadRequest(responseDto.Message);
            }

            return disable ? Ok("Mentee is disabled.")
                : Ok("Mentee is enabled.");
        }



        /// <summary>
        /// Delete a mentee from database.
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
            BaseResponseDto responseDto = null;

            if (menteeId == null)
            {
                return BadRequest("Mentee id must not be null");
            }

            try
            {
                responseDto = _mentee.Delete(menteeId);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            if (responseDto.Status == 1 || responseDto.Status == 2)
            {
                return BadRequest(responseDto.Message);
            }

            return Ok(responseDto.Message);
        }



        //  Specialized APIs

        /// <summary>
        /// Get a mentee's list of subscribed channels
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
        [HttpGet("subs/{menteeId}")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 500
        public IActionResult GetSubbedChannel(string menteeId)
        {
            BaseResponseDto<MenteeSubbedChannelModel> responseDto = null;
            ICollection<MenteeSubbedChannelModel> result = null;

            if (menteeId == null)
            {
                return BadRequest("Mentee Id must not be null");
            }

            try
            {
                responseDto = _mentee.GetSubbedChannels(menteeId);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }


            if (responseDto.Status == 1)
            {
                return BadRequest(responseDto.Message);
            }

            if (responseDto.Status == 2)
            {
                return Ok(responseDto.Message);
            }

            //  finalize
            result = responseDto.Content.ToList();
            return Ok(result);
        }




        /// <summary>
        /// Get number of enroll per mentee
        /// </summary>
        ///
        /// <returns>
        /// A list of mentees, each contain the number of enrolls
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Not have enough infomation</response>
        /// <response code="401">Unauthorize</response>
        /// <response code="403">Forbidden from resource</response>
        /// <response code="500">Internal Error</response>
        [HttpGet("enroll")]
        #region repCode 200 400 401 403 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 500
        public IActionResult GetEnroll()
        {

            BaseResponseDto<MenteeEnrollCountModel> responseDto = null;
            ICollection<MenteeEnrollCountModel> result = null;

            try
            {
                responseDto = _mentee.CountEnroll();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            if (responseDto.Status == 1 || responseDto.Status == 2)
            {
                return Ok(responseDto.Message);
            }

            result = responseDto.Content.ToList();

            return Ok(result);
        }
    }
}
