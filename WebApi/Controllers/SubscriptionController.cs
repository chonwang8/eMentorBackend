using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.Models.SubscriptionModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Domain.DTO.ResponseDtos;
using System;

namespace WebApi.Controllers
{
    [Route("api/subscriptions")]
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
        /// Get list of subscriptions.
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
        public IActionResult GetAll()
        {
            ICollection<SubscriptionViewModel> result = _subscription.GetAll().ToList();
            if (result == null || result.Count == 0)
            {
                return Ok("There are no subscription in the system");
            }

            return Ok(result);
        }



        /// <summary>
        /// Get subscription by Id.
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

            ICollection<SubscriptionModel> result = _subscription.GetById(subscriptionId).ToList();

            if (result == null || result.Count == 0)
            {
                return NotFound("No Subscription with Id " + subscriptionId + " found.");
            }

            return Ok(result);
        }



        /// <summary>
        /// Insert a subscription into database.
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
        public IActionResult Insert(SubscriptionInsertModel subscriptionInsertModel)
        {
            if (subscriptionInsertModel == null)
            {
                return BadRequest("Subscription info must not be null");
            }

            int result = _subscription.Insert(subscriptionInsertModel);

            if (result == 0)
            {
                return BadRequest("Faulthy Subscription info.");
            } else if (result ==1)
            {
                return BadRequest("Already subscribed to this channel");
            }

            return Ok("Subscription Inserted");
        }



        /// <summary>
        /// Update an existing subscription.
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
        public IActionResult Update(SubscriptionUpdateModel subscriptionUpdateModel)
        {
            if (subscriptionUpdateModel == null)
            {
                return BadRequest("Subscription info must not be null");
            }

            int result = _subscription.Update(subscriptionUpdateModel);

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
        /// Change status of a subscription (Disabled/Enabled).
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
        /// Delete a subscription from database.
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
        [HttpDelete("admin/{subscriptionId}")]
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



        /// <summary>
        /// Unsubscribe a mentee from a channel.
        /// </summary>
        /// 
        /// <param name="menteeId">
        /// The mentee's identifier.
        /// </param>
        /// <param name="channelId">
        /// The channel's identifier.
        /// </param>
        /// 
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Subscription with matching Id not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{menteeId}/{channelId}")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
        public IActionResult Delete(string menteeId, string channelId)
        {
            BaseResponseDto responseDto = null;

            if (menteeId == null)
            {
                return BadRequest("Channel Id must not be null");
            }
            if (channelId == null)
            {
                return BadRequest("Channel Id must not be null");
            }

            try
            {
                responseDto = _subscription.Delete(menteeId, channelId);
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
