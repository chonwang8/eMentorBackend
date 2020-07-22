using Domain.DTO;
using Domain.DTO.QueryAttributesDtos;
using Domain.DTO.ResponseDtos;
using Domain.Services.Interfaces;
using Domain.Models.ChannelModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/channels")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ChannelsController : ControllerBase
    {
        #region Classes - Constructors
        protected readonly IChannelService _channel;

        public ChannelsController(IChannelService channel)
        {
            _channel = channel;
        }
        #endregion


        /// <summary>
        /// Get list of channels.
        /// </summary>
        /// 
        /// <returns>
        /// List containing channels. Message if list is empty.
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
            BaseResponseDto<ChannelViewModel> responseDto = null;
            ICollection<ChannelViewModel> result = null;

            try
            {
                responseDto = _channel.GetAll();
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
        /// Get channel by Id.
        /// </summary>
        /// <param name="channelId">
        /// The channel's identifier.
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
        [HttpGet("{channelId}")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 500
        public IActionResult GetById(string channelId)
        {
            BaseResponseDto<ChannelModel> responseDto = null;
            ICollection<ChannelModel> result = null;

            if (channelId == null)
            {
                return BadRequest("Channel Id must not be null");
            }

            try
            {
                responseDto = _channel.GetById(channelId);
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
        /// Insert a channel into database.
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
        public IActionResult Insert(ChannelInsertModel channelInsertModel)
        {
            BaseResponseDto responseDto = null;

            if (channelInsertModel == null)
            {
                return BadRequest("Sharing info must not be null");
            }

            try
            {
                responseDto = _channel.Insert(channelInsertModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            return Ok(responseDto.Message);
        }



        /// <summary>
        /// Update an existing channel.
        /// </summary>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Channel with matching Id not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        #region repCode 200 400 401 403 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
        public IActionResult Update(ChannelUpdateModel channelUpdateModel)
        {
            BaseResponseDto responseDto = null;

            if (channelUpdateModel == null)
            {
                return BadRequest("Sharing info must not be null");
            }

            try
            {
                responseDto = _channel.Update(channelUpdateModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            return Ok(responseDto.Message);
        }



        /// <summary>
        /// Change status of a channel (Disabled/Enabled).
        /// </summary>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Channel with matching Id not found</response>
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
        public IActionResult ChangeStatus(string channelId, bool? isDisable)
        {
            BaseResponseDto responseDto = null;

            if (channelId == null)
            {
                return BadRequest("Channel Id must not be null.");
            }
            if (isDisable.Equals(null))
            {
                return BadRequest("Must specify isDisable parameter in order to allow this function works correctly");
            }

            bool disable = isDisable.Value;

            try
            {
                responseDto = _channel.ChangeStatus(channelId, disable);
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
        /// Delete a channel from database.
        /// </summary>
        /// <param name="channelId">
        /// The channel's identifier.
        /// </param>
        /// <returns>
        /// Message
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Channel with matching Id not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{channelId}")]
        #region repCode 200 400 401 403 404 500
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion repCode 200 400 401 403 500
        public IActionResult Delete(string channelId)
        {
            BaseResponseDto responseDto = null;

            if (channelId == null)
            {
                return BadRequest("Channel Id must not be null");
            }

            try
            {
                responseDto = _channel.Delete(channelId);
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




        #region Specialized APIs
        //  Keep
        [HttpGet("topic")]
        public IActionResult GetChannelByTopicId(List<Guid> TopicId)
        {
            return Ok(_channel.GetChannelByTopicId(TopicId));
        }


        //  Wang - hot fix
        [HttpGet("subcribe")]
        public IActionResult GetChannelSubCount(string channelId)
        {
            var result = _channel.GetChannelSubCount(channelId);
            return Ok(result);
        }
        #endregion
    }
}