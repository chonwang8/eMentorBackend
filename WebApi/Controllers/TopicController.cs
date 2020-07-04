﻿using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/topics")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class TopicController : ControllerBase
    {
        #region Classes - Constructors
        protected readonly ITopicService _topic;
        public TopicController(ITopicService service)
        {
            _topic = service;
        }
        #endregion



        /// <summary>
        /// Get list of topics. GET "api/topics"
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
        /// List containing topics. Message if list is empty.
        /// </returns>
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

            List<TopicViewModel> result = _topic.GetAll(paging).ToList();
            if (result == null || result.Count == 0)
            {
                return Ok("There are no topics in the system");
            }
            return Ok(result);
        }



        /// <summary>
        /// Get topic by Id.  GET "api/topics/{topicId}"
        /// </summary>
        /// <param name="topicId">
        /// The topic's identifier.
        /// </param>
        /// <returns>
        /// Topic with matching Id
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Topic with matching Id not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{topicId}")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 500
        public IActionResult Get(string topicId)
        {
            if (topicId == null)
            {
                return BadRequest("User info must not be null");
            }

            List<TopicViewModel> result = _topic.GetById(topicId).ToList();

            if (result == null || result.Count == 0)
            {
                return NotFound("No topic with ID " + topicId + " was found.");
            }

            return Ok(result);
        }



        /// <summary>
        /// Insert a topic into database. POST "api/topics".
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
        public IActionResult Insert(TopicViewModel topic)
        {
            if (topic == null)
            {
                return BadRequest("Topic info must not be null");
            }

            int result = _topic.Insert(topic);

            if (result == 0)
            {
                return BadRequest("Faulthy topic info.");
            }
            if (result == 1)
            {
                return BadRequest("This topic is already in the system");
            }

            return Ok("Inserted topic " + topic.TopicName);
        }



        /// <summary>
        /// Update an existing topic. PUT "api/topics".
        /// </summary>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Topic with matching Id not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        #region repCode 200 400 401 403 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
        public IActionResult Update(TopicViewModel topic)
        {
            if (topic == null)
            {
                return BadRequest("Topic info must not be null");
            }

            int result = _topic.Update(topic);

            if (result == 0)
            {
                return BadRequest("Faulthy topic info.");
            }
            if (result == 1)
            {
                return NotFound("Topic not found");
            }

            return Ok("Updated topic " + topic.TopicName);
        }



        /// <summary>
        /// Change status of a topic (Disabled/Enabled). PUT "api/users/status".
        /// </summary>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Topic with matching Id not found</response>
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
        public IActionResult ChangeStatus(string topicId, bool isDisable)
        {
            if (topicId == null)
            {
                return BadRequest("SubscriptionId must not be null.");
            }
            
            int result = _topic.ChangeStatus(topicId, isDisable);

            if (result == 0)
            {
                return BadRequest("Faulthy TopicId.");
            }
            if (result == 1)
            {
                return NotFound("Topic not found");
            }

            return isDisable ? Ok("Topic is disabled.")
                : Ok("Topic is enabled.");
        }



        /// <summary>
        /// Delete a topic from database.. DELETE "api/topics/{topicId}".
        /// </summary>
        /// <param name="topicId">
        /// The topic's identifier.
        /// </param>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Topic with matching Id not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{topicId}")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
        public IActionResult Delete(string topicId)
        {
            if (topicId == null)
            {
                return BadRequest("User info must not be null");
            }

            int result = _topic.Delete(topicId);

            if (result == 0)
            {
                return BadRequest("Faulthy topic info.");
            }
            if (result == 1)
            {
                return NotFound("Topic not found");
            }

            return Ok("Topic is deleted.");
        }
    }
}
