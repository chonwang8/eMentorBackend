using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.ViewModels.SubscriptionModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/subscription")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        #region Classes - Constructors
        protected readonly ISubscriptionService _subscription;
        public SubscriptionController(ISubscriptionService subscription)
        {
            _subscription = subscription;
        }
        #endregion



        /// <summary>
        /// Get list of subscriptions. GET "api/subscriptions"
        /// </summary>
        /// <returns>
        /// List containing subscriptions.
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        #region repCode 200 400 401 403 404 500
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

            List<SubscriptionViewModel> result = _subscription.GetAll(paging).ToList();
            if (result == null || result.Count == 0)
            {
                return Ok("There are no subscription in the system");
            }

            return Ok(result);
        }



        /// <summary>
        /// Get user by Id. GET "api/subscriptions/{subscriptionId}"
        /// </summary>
        /// <param name="subscriptionId">
        /// The subscription identifier.
        /// </param>
        /// <returns>
        /// Subscription with matching Id
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">User with matching Id not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("{subscriptionId}")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 500
        public IActionResult GetById(string subscriptionId)
        {
            if (subscriptionId == null)
            {
                return BadRequest("Subscription ID info must not be null");
            }

            List<SubscriptionViewModel> result = _subscription.GetById(subscriptionId).ToList();

            if (result == null || result.Count == 0)
            {
                return NotFound("No Subscription with Id " + subscriptionId + " found.");
            }

            return Ok(result);
        }



        /// <summary>
        /// Insert a subscription into database. POST "api/subscriptions"
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
        public IActionResult Insert(SubscriptionViewModel subscriptionViewModel)
        {
            if (subscriptionViewModel == null)
            {
                return BadRequest("Subscription info must not be null");
            }

            int result = _subscription.Insert(subscriptionViewModel);

            if (result == 0)
            {
                return BadRequest("Faulthy Subscription info.");
            }
            if (result == 1)
            {
                return BadRequest("This Subscription already existed");
            }

            return Ok("Subscription Inserted");
        }



        /// <summary>
        /// Update an existing subscription. PUT "api/subscriptions".
        /// </summary>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Subscription with matching Id not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        #region repCode 200 400 401 403 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
        public IActionResult Update(SubscriptionViewModel subscriptionViewModel)
        {
            if (subscriptionViewModel == null)
            {
                return BadRequest("Subscription info must not be null");
            }

            int result = _subscription.Insert(subscriptionViewModel);

            if (result == 0)
            {
                return BadRequest("Faulthy Subscription info.");
            }
            if (result == 1)
            {
                return NotFound("Subscription not found");
            }

            return Ok("Subscription updated");
        }



        /// <summary>
        /// Change status of a subscription (Disabled/Enabled). PUT "api/subscriptions/status".
        /// </summary>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Subscription with matching Id not found</response>
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
        public IActionResult ChangeStatus(string subscriptionId, bool isDisable)
        {
            if (subscriptionId == null)
            {
                return BadRequest("SubscriptionId must not be null.");
            }

            int result = _subscription.ChangeStatus(subscriptionId, isDisable);

            if (result == 0)
            {
                return BadRequest("Faulthy SubscriptionId.");
            }
            if (result == 1)
            {
                return NotFound("Subscription not found");
            }

            return isDisable ? Ok("Subscription is disabled.")
                : Ok("Subscription is enabled.");
        }



        /// <summary>
        /// Delete a subscription from database. DELETE "api/users/{userId}".
        /// </summary>
        /// <param name="subscriptionId">
        /// The subscription's identifier.
        /// </param>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Subscription with matching Id not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{subscriptionId}")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
        public IActionResult Delete(string subscriptionId)
        {
            if (subscriptionId == null)
            {
                return BadRequest("SubscriptionId must not be null");
            }

            int result = _subscription.Delete(subscriptionId);

            if (result == 0)
            {
                return BadRequest("Faulthy Subscription info.");
            }
            if (result == 1)
            {
                return NotFound("Subscription not found");
            }

            return Ok("Subscription is deleted.");
        }



    }
}
