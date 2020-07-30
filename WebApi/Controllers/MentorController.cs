using Domain.DTO.ResponseDtos;
using Domain.Services.Interfaces;
using Domain.Models.MentorModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models.RatingModels;

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
        /// Get list of mentors.
        /// </summary>
        /// 
        /// <returns>
        /// List containing mentors. Message if list is empty.
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
            BaseResponseDto<MentorViewModel> responseDto = null;
            ICollection<MentorViewModel> result = null;

            try
            {
                responseDto = _mentor.GetAll();
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
        /// Get mentor by Id.
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
            BaseResponseDto<MentorModel> responseDto = null;
            ICollection<MentorModel> result = null;

            if (mentorId == null)
            {
                return BadRequest("Mentor Id must not be null");
            }

            try
            {
                responseDto = _mentor.GetById(mentorId);
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
        /// Get Mentor Id by Email
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
                responseDto = _mentor.GoogleLogin(email);
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
        /// Insert a mentor into database. Returns a JWT
        /// </summary>
        /// <returns>
        /// JWT token || error message
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
        public IActionResult Insert(MentorInsertModel mentorInsertModel)
        {
            BaseResponseDto responseDto = null;

            if (mentorInsertModel == null)
            {
                return BadRequest("Mentor info must not be null");
            }

            try
            {
                responseDto = _mentor.Insert(mentorInsertModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            return Ok(responseDto.Message);
        }



        /// <summary>
        /// Update an existing mentor.
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
        public IActionResult Update(MentorUpdateModel mentorModel)
        {
            BaseResponseDto responseDto = null;

            if (mentorModel == null)
            {
                return BadRequest("Mentor info must not be null");
            }

            try
            {
                responseDto = _mentor.Update(mentorModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message + "  \n  \n  \n  " + e.StackTrace);
            }

            return Ok(responseDto.Message);
        }



        /// <summary>
        /// Change status of a mentor (Disabled/Enabled).
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
        public IActionResult ChangeStatus(string mentorId, bool? isDisable)
        {
            BaseResponseDto responseDto = null;

            if (mentorId == null)
            {
                return BadRequest("Mentor Id must not be null.");
            }
            if (isDisable.Equals(null))
            {
                return BadRequest("Must specify isDisable parameter in order to allow this function works correctly");
            }

            bool disable = isDisable.Value;

            try
            {
                responseDto = _mentor.ChangeStatus(mentorId, disable);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            if (responseDto.Status == 1 || responseDto.Status == 2)
            {
                return BadRequest(responseDto.Message);
            }

            return disable ? Ok("Mentor is disabled.")
                : Ok("Mentor is enabled.");
        }



        /// <summary>
        /// Delete a mentor from database.
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
            BaseResponseDto responseDto = null;

            if (mentorId == null)
            {
                return BadRequest("Mentor id must not be null");
            }

            try
            {
                responseDto = _mentor.Delete(mentorId);
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
        /// Insert a rating feeed into database.
        /// </summary>
        /// 
        /// <returns>
        /// Message
        /// </returns>
        /// 
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("rating")]
        #region repCode 200 400 401 403 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
        public IActionResult InsertRating(RatingInsertModel ratingInsertModel)
        {
            BaseResponseDto responseDto = null;

            if (ratingInsertModel == null)
            {
                return BadRequest("Faulthy input must not be null");
            }

            try
            {
                responseDto = _mentor.InsertRating(ratingInsertModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return StatusCode(responseDto.Status, responseDto.Message);
        }



        ///// <summary>
        ///// Insert a rating feeed into database.
        ///// </summary>
        ///// 
        ///// <returns>
        ///// Message
        ///// </returns>
        ///// 
        ///// <response code="200">Success</response>
        ///// <response code="400">Bad Request</response>
        ///// <response code="401">Unauthorized</response>
        ///// <response code="403">Forbidden</response>
        ///// <response code="500">Internal server error</response>
        //[HttpPost("emptyRatingInsert")]
        //#region repCode 200 400 401 403 500
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //#endregion repCode 200 400 401 403 500
        //public IActionResult InsertEmptyRating(string mentorId)
        //{
        //    BaseResponseDto responseDto = null;

        //    if (mentorId == null)
        //    {
        //        return BadRequest("Faulthy input must not be null");
        //    }

        //    try
        //    {
        //        responseDto = _mentor.InsertRating(mentorId);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e);
        //    }
        //    return StatusCode(responseDto.Status, responseDto.Message);
        //}


        //public IActionResult CountMenteeEnrollSharing()
        //{
        //    if (_mentor.CountMenteeEnrollSharing() == null)
        //        return BadRequest();
        //    return Ok(_mentor.CountMenteeEnrollSharing());
        //}


        //public IActionResult CountSharingByMentor()
        //{
        //    if (_mentor.CountSharingByMentor() == null)
        //        return BadRequest();
        //    return Ok(_mentor.CountSharingByMentor());
        //}

    }
}
