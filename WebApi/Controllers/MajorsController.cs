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
    public class MajorsController : ControllerBase
    {
        protected readonly IMajorService _service;
        public MajorsController(IMajorService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetTopicGroupByMajor()
        {
            return Ok(_service.GetTopicGroupByMajor());
        }
    }
}