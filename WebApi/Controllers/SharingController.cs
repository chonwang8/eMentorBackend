using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.ViewModels.SharingModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        /// Get list of users. GET "api/sharings"
        /// </summary>
        /// 
        /// <param name="size">
        /// The number of items on a page. If null will be 40 by default.
        /// </param>
        /// <param name="index">
        /// The page number where paging is started. If null will be 1 by default.
        /// </param>
        /// <param name="asc">
        /// Boolean value determining whether return list will be ascending or not. If null will be false by default.
        /// </param>
        /// <param name="isApproved">
        /// Boolean value determining whether return list will include sharings that are approved or not. If null will be false by default.
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
        public IActionResult GetAll(string size, string index, bool asc, bool isApproved)
        {
            int pageSize, pageIndex;
            GetAllDTO request = null;

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

            #endregion

            request = new GetAllDTO
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                IsAscending = asc,
                IsApproved = isApproved
            };

            List<SharingViewModel> result = _sharing.GetAll(request).ToList();
            
            if (result == null || result.Count == 0)
            {
                return Ok("There are no sharings in the system");
            }

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
        public IActionResult Insert(SharingModel sharingViewModel)
        {
            if (sharingViewModel == null)
            {
                return BadRequest("Sharing info must not be null");
            }

            int result = _sharing.Insert(sharingViewModel);

            if (result == 0)
            {
                return BadRequest("Faulthy sharing info.");
            }
            if (result == 1)
            {
                return BadRequest("This sharing is already existed");
            }

            return Ok("Sharing Created");
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
        public IActionResult Update(SharingModel sharingViewModel)
        {
            if (sharingViewModel == null)
            {
                return BadRequest("Sharing info must not be null");
            }

            int result = _sharing.Insert(sharingViewModel);

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
