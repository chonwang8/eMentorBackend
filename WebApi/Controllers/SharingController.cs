using Domain.DTO.QueryAttributesDtos;
using Domain.DTO.ResponseDtos;
using Domain.Services.Interfaces;
using Domain.ViewModels.SharingModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/sharings")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class SharingController : ControllerBase
    {
        #region Classes - Constructors
        protected readonly ISharingService _sharing;
        public SharingController(ISharingService service)
        {
            _sharing = service;
        }
        #endregion



        /// <summary>
        /// Get list of sharings.
        /// </summary>
        /// 
        /// <param name="ascending">
        /// (True(1)/False(0)) Boolean value determining whether return list will be ascending or not. May left null.
        /// </param>
        /// <param name="approved">
        /// (True(1)/False(0)) Boolean value determining whether return list will include sharings that are approved or not. May left null.
        /// </param>
        /// 
        /// <returns>
        /// List containing sharings. Message if list is empty.
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
        public IActionResult GetAll(bool? ascending = null, bool? approved = null)
        {
            BaseResponseDto<SharingViewModel> responseDto = null;
            ICollection<SharingViewModel> result = null;

            FilterDto filterRequest = new FilterDto
            {
                IsApproved = approved,
                IsAscending = ascending
            };

            try
            {
                responseDto = _sharing.GetAll(filterRequest);
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
        /// Get sharing by Id.
        /// </summary>
        /// <param name="sharingId">
        /// The sharing's identifier.
        /// </param>
        /// <returns>
        /// Sharing with matching Id
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Sharing with matching Id not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{sharingId}")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 500
        public IActionResult GetById(string sharingId)
        {
            BaseResponseDto<SharingModel> responseDto = null;
            ICollection<SharingModel> result = null;

            if (sharingId == null)
            {
                return BadRequest("Sharing info must not be null");
            }

            try
            {
                responseDto = _sharing.GetById(sharingId);
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
        /// Insert a sharing into database.
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
        public IActionResult Insert(SharingInsertModel sharingInsertModel)
        {
            BaseResponseDto responseDto = null;

            if (sharingInsertModel == null)
            {
                return BadRequest("Sharing info must not be null");
            }

            try
            {
                responseDto = _sharing.Insert(sharingInsertModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            return Ok(responseDto.Message);
        }



        /// <summary>
        /// Update an existing sharing.
        /// </summary>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Sharing with matching Id not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        #region repCode 200 400 401 403 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
        public IActionResult Update(SharingModel sharingModel)
        {
            BaseResponseDto responseDto = null;

            if (sharingModel == null)
            {
                return BadRequest("Sharing info must not be null");
            }

            try
            {
                responseDto = _sharing.Update(sharingModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            return Ok(responseDto.Message);
        }



        /// <summary>
        /// Change status of a sharing (Disabled/Enabled).
        /// </summary>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Sharing with matching Id not found</response>
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
        public IActionResult ChangeStatus(string sharingId, bool? isDisable)
        {
            BaseResponseDto responseDto = null;

            if (sharingId == null)
            {
                return BadRequest("SharingId must not be null.");
            }
            if (isDisable.Equals(null))
            {
                return BadRequest("Must specify isDisable parameter in order to allow this function works correctly");
            }

            bool disable = isDisable.HasValue;

            try
            {
                responseDto = _sharing.ChangeStatus(sharingId, disable);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            if (responseDto.Status == 1 || responseDto.Status == 2)
            {
                return BadRequest(responseDto.Message);
            }

            return disable ? Ok("Sharing is disabled.")
                : Ok("Sharing is enabled.");
        }



        /// <summary>
        /// Delete a sharing from database.
        /// </summary>
        /// <param name="sharingId">
        /// The sharing's identifier.
        /// </param>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Sharing with matching Id not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{sharingId}")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
        public IActionResult Delete(string sharingId)
        {
            BaseResponseDto responseDto = null;

            if (sharingId == null)
            {
                return BadRequest("Sharing info must not be null");
            }

            try
            {
                responseDto = _sharing.Delete(sharingId);
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
