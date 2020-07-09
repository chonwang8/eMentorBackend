using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.ViewModels.ChannelModels;
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
    public class ChannelsController : ControllerBase
    {
        #region Classes - Constructors
        protected readonly IChannelService _service;
        public ChannelsController(IChannelService service)
        {
            _service = service;
        }
        #endregion


        [HttpGet]
        public IActionResult GetAll(string size, string index, string asc)
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

            return Ok(_service.GetAll(request));
        }

        [HttpPost]
        public IActionResult Insert(ChannelInsertModel channel)
        {
            _service.Insert(channel);
            return Ok("Created Successfully !");
        }

        [HttpPut]
        public IActionResult Update(UpdateChannelDTO channel)
        {
            if (_service.Update(channel) == false)
                return BadRequest("Updated Failed !");
            return Ok("Updated Successfully !");
        }

        [HttpDelete("{ChannelId}")]
        public IActionResult Delete(Guid ChannelId)
        {
            if (_service.Delete(ChannelId) == false)
                return BadRequest("Deleted Failed !");
            return Ok("Deleted Successfully !");
        }




        //  Keep
        [HttpGet("topic")]
        public IActionResult GetChannelByTopicId(List<Guid> TopicId)
        {
            return Ok(_service.GetChannelByTopicId(TopicId));
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