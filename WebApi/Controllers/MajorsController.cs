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
        public IActionResult GetAllMajor(string size, string index, string asc)
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
            else
            {
                IsAscended = false;
            }

            #endregion

            paging = new GetAllDTO
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                IsAscending = false
            };

            return Ok(_service.GetAllMajor(paging));
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