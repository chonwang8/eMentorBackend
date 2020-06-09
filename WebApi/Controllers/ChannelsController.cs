using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelsController : ControllerBase
    {
        protected readonly IChannelService _service;
        public ChannelsController(IChannelService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetAllChannel()
        {
            return Ok(_service.GetAllChannel());
        }

        [HttpPost]
        public IActionResult GetChannelByTopicId(List<Guid> TopicId)
        {
            return Ok(_service.GetChannelByTopicId(TopicId));
        }
    }
}