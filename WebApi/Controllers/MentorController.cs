using Domain.DTO.QueryAttributesDtos;
using Domain.DTO.ResponseDtos;
using Domain.Services.Interfaces;
using Domain.ViewModels.MentorModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
        /// <param name="size">
        /// The number of items on a page. If null will be 40 by default.
        /// </param>
        /// <param name="index">
        /// The page number where paging is started. If null will be 1 by default.
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
        public IActionResult GetAll(int? size = null, int? index = null)
        {
            BaseResponseDto<MentorViewModel> responseDto = null;
            ICollection<MentorViewModel> result = null;

            PagingDto pagingRequest = new PagingDto
            {
                PageIndex = index,
                PageSize = size
            };

            try
            {
                responseDto = _mentor.GetAll(pagingRequest);
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
        /// Insert a mentor into database.
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
        public IActionResult Insert(MentorInsertModel mentorModel)
        {
            BaseResponseDto responseDto = null;

            if (mentorModel == null)
            {
                return BadRequest("Mentor info must not be null");
            }

            try
            {
                responseDto = _mentor.Insert(mentorModel);
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
                return StatusCode(500, e);
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

            bool disable = isDisable.HasValue;

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



    }
}
