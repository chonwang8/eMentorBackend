using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.Models.EnrollModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.DTO.ResponseDtos;

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
        /// Get list of enrolls.
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
            BaseResponseDto<EnrollViewModel> responseDto = null;
            ICollection<EnrollViewModel> result = null;

            try
            {
                responseDto = _enroll.GetAll();
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
        /// Get enroll by Id.
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
            BaseResponseDto<EnrollModel> responseDto = null;
            ICollection<EnrollModel> result = null;

            if (enrollId == null)
            {
                return BadRequest("Enroll Id must not be null");
            }

            try
            {
                responseDto = _enroll.GetById(enrollId);
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
        /// Insert an enroll into database.
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
            BaseResponseDto responseDto = null;

            if (enrollInsertModel == null)
            {
                return BadRequest("Enroll info must not be null");
            }

            try
            {
                responseDto = _enroll.Insert(enrollInsertModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            return Ok(responseDto.Message);
        }



        /// <summary>
        /// Update an existing enroll.
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
        public IActionResult Update(EnrollUpdateModel enrollUpdateModel)
        {
            BaseResponseDto responseDto = null;

            if (enrollUpdateModel == null)
            {
                return BadRequest("Enroll info must not be null");
            }

            try
            {
                responseDto = _enroll.Update(enrollUpdateModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            return Ok(responseDto.Message);
        }



        /// <summary>
        /// Change status of an enroll (Disabled/Enabled).
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
        public IActionResult ChangeStatus(string enrollId, bool? isDisable)
        {
            BaseResponseDto responseDto = null;

            if (enrollId == null)
            {
                return BadRequest("Enroll Id must not be null.");
            }
            if (isDisable.Equals(null))
            {
                return BadRequest("Must specify isDisable parameter in order to allow this function works correctly");
            }

            bool disable = isDisable.HasValue;

            try
            {
                responseDto = _enroll.ChangeStatus(enrollId, disable);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            if (responseDto.Status == 1 || responseDto.Status == 2)
            {
                return BadRequest(responseDto.Message);
            }

            return disable ? Ok("Channel is disabled.")
                : Ok("Channel is enabled.");
        }



        /// <summary>
        /// Delete an enroll from database.
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
            BaseResponseDto responseDto = null;

            if (enrollId == null)
            {
                return BadRequest("Enroll Id must not be null");
            }

            try
            {
                responseDto = _enroll.Delete(enrollId);
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
