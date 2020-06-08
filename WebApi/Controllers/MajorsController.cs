using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/majors")]
    [ApiController]
    public class MajorsController : ControllerBase
    {
        protected readonly IMajorService _service;
        public MajorsController(IMajorService service)
        {
            _service = service;
        }

        [HttpGet("topics")]
        public IActionResult GetTopicGroupByMajor()
        {
            return Ok(_service.GetTopicGroupByMajor());
        }

        [HttpGet]
        public IActionResult GetAllMajor()
        {
            return Ok(_service.GetAllMajor());
        }
        
        [HttpPost]
        public IActionResult GetMajorById(Guid MajorId)
        {
            if(GetMajorById(MajorId) == null)
            {
                return NotFound("Not found Major for this ID");
            }
            return Ok(_service.GetMajorById(MajorId));
        }
    }
}