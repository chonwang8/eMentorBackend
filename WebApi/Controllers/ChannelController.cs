using Domain.DTO;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/channels")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ChannelController : ControllerBase
    {
        #region Classes - Constructors
        protected readonly IChannelService _service;
        public ChannelController(IChannelService service)
        {
            _service = service;
        }
        #endregion


        [HttpGet]
        public IActionResult GetAllChannel(string size, string index, string asc)
        {
            int pageSize, pageIndex;
            bool IsAscended = false;
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

            if (!string.IsNullOrWhiteSpace(asc))
            {
                if (!asc.ToLower().Equals("true") || !asc.ToLower().Equals("false"))
                {
                    return BadRequest("Invalid paging values");
                }
                IsAscended = bool.Parse(asc);
            }

            #endregion

            request = new GetAllDTO
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                IsAscending = false
            };

            return Ok(_service.GetAllChannel(request));
        }

        [HttpPost]
        public IActionResult CreateChannel(CreateChannelDTO channel)
        {
            _service.CreateChannel(channel);
            return Ok("Created Successfully !");
        }

        [HttpGet("TopicId")]
        public IActionResult GetChannelByTopicId(List<Guid> TopicId)
        {
            return Ok(_service.GetChannelByTopicId(TopicId));
        }

        [HttpDelete("{ChannelId}")]
        public IActionResult DeleteChannelById(Guid ChannelId)
        {
            if (_service.DeleteChannelById(ChannelId) == false)
                return BadRequest("Deleted Failed !");
            return Ok("Deleted Successfully !");
        }

        [HttpPut]
        public IActionResult UpdateChannelById(UpdateChannelDTO channel)
        {
            if (_service.UpdateChannelById(channel) == false)
                return BadRequest("Updated Failed !");
            return Ok("Updated Successfully !");
        }


        //  Wang - hot fix
        [HttpGet("subcribe")]
        public IActionResult GetChannelSubCount(string channelId)
        {
            var result = _service.GetChannelSubCount(new Guid(channelId));
            return Ok(result);
        }
    }
}