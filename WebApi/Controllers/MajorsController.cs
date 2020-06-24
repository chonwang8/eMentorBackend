using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/majors")]
    [ApiController]
    [EnableCors("MyPolicy")]
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
        public IActionResult GetAllMajor(GetAllDTO request)
        {
            return Ok(_service.GetAllMajor(request));
        }

        [HttpGet("{MajorId}")]
        public IActionResult GetMajorById(Guid MajorId)
        {
            if(_service.GetMajorById(MajorId) == null)
            {
                return NotFound("Not found Major for this ID");
            }
            return Ok(_service.GetMajorById(MajorId));
        }
        
        [HttpDelete]
        public IActionResult DeleteMajorById(Guid MajorId)
        {
            if(_service.DeleteMajorById(MajorId) == false)
                return BadRequest("Deleted Failed !");   
            return Ok("Deleted Successfully !");
        } 
        
        [HttpPut]
        public IActionResult UpdateMajorById(UpdateMajorDTO major)
        {
            if(_service.UpdateMajorById(major) == false)
                return BadRequest("Updated Failed !");
            return Ok("Updated Successfully !");
        } 
        
        [HttpPost]
        public IActionResult CreateMajor(Guid MajorId)
        {
            return Ok("Created Successfully !");
        }
    }
}