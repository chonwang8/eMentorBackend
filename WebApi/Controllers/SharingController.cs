using Domain.DTO;
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
        /// <param name="size">
        /// The number of items on a page. May left null. Must co-exist with index.
        /// </param>
        /// <param name="index">
        /// The page number where paging is started. May left null. Must co-exist with size.
        /// </param>
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
        public IActionResult GetAll(int? size = null, int? index = null, bool? ascending = null, bool? approved = null)
        {
            PagingDto pagingRequest = new PagingDto
            {
                PageIndex = index,
                PageSize = size
            };

            FilterDto filterRequest = new FilterDto
            {
                IsApproved = approved,
                IsAscending = ascending
            };

            SharingResponseDto<SharingViewModel> responseDto = null;
            ICollection<SharingViewModel> result = null;

            try
            {
                responseDto = _sharing.GetAll(pagingRequest, filterRequest);
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
        /// Get user by Id. GET "api/sharings/{sharingId}"
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
            if (sharingId == null)
            {
                return BadRequest("Sharing info must not be null");
            }

            List<SharingModel> result = _sharing.GetById(sharingId).ToList();

            if (result == null || result.Count == 0)
            {
                return NotFound("No sharing(s) with ID " + sharingId + " found.");
            }

            return Ok(result);
        }



        /// <summary>
        /// Insert a sharing into database. POST "api/sharings".
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
            SharingResponseDto responseDto = null;

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
        /// Update an existing sharing. PUT "api/sharings".
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
            if (sharingModel == null)
            {
                return BadRequest("Sharing info must not be null");
            }

            int result;

            try
            {
                result = _sharing.Update(sharingModel);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }

            if (result == 0)
            {
                return BadRequest("Faulthy sharing info.");
            }
            if (result == 1)
            {
                return NotFound("Sharing not found");
            }

            return Ok("Sharing information updated");
        }



        /// <summary>
        /// Change status of a sharing (Disabled/Enabled). PUT "api/sharings/status".
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
        public IActionResult ChangeStatus(string sharingId, bool isDisable)
        {
            if (sharingId == null)
            {
                return BadRequest("SharingId must not be null.");
            }

            int result = _sharing.ChangeStatus(sharingId, isDisable);

            if (result == 0)
            {
                return BadRequest("Faulthy SharingId.");
            }
            if (result == 1)
            {
                return NotFound("Sharing not found");
            }

            return isDisable ? Ok("Sharing is disabled.")
                : Ok("Sharing is enabled.");
        }



        /// <summary>
        /// Delete a sharing from database. DELETE "api/sharings/{sharingId}".
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
            if (sharingId == null)
            {
                return BadRequest("Sharing info must not be null");
            }

            int result = _sharing.Delete(sharingId);

            if (result == 0)
            {
                return BadRequest("Faulthy sharing info.");
            }
            if (result == 1)
            {
                return NotFound("Sharing was not found");
            }

            return Ok("Sharing is deleted.");
        }



    }
}
