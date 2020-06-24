using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/mentees")]
    [ApiController]
    public class MenteeController : ControllerBase
    {
        protected readonly IMenteeService _mentee;
        public MenteeController(IMenteeService service)
        {
            _mentee = service;
        }


        [HttpGet]
        public IActionResult GetAll(GetAllDTO request)
        {
            List<MenteeViewModel> result = _mentee.GetAll(request).ToList();
            if (result == null || result.Count == 0)
            {
                return Ok("There are no mentees in the system");
            }
            return Ok(result);
        }


        [HttpPost("{id}")]
        public IActionResult GetById(string menteeId)
        {
            if (menteeId == null)
            {
                return BadRequest("Mentee info must not be null");
            }

            List<MenteeViewModel> result = _mentee.GetById(menteeId).ToList();

            if (result == null || result.Count == 0)
            {
                return NotFound("No mentee with Id " + menteeId + " found.");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Insert(MenteeViewModel mentee)
        {
            if (mentee == null)
            {
                return BadRequest("Mentee info must not be null");
            }

            int result = _mentee.Insert(mentee);

            if (result == 0)
            {
                return BadRequest("Faulthy mentee info.");
            }
            if (result == 1)
            {
                return BadRequest("This user is already a mentee");
            }

            return Ok("Mentee Inserted");
        }

        [HttpPut]
        public IActionResult Update(MenteeViewModel mentee)
        {
            if (mentee == null)
            {
                return BadRequest("Mentee info must not be null");
            }

            int result = _mentee.Insert(mentee);

            if (result == 0)
            {
                return BadRequest("Faulthy mentee info.");
            }
            if (result == 1)
            {
                return NotFound("Mentee not found");
            }

            return Ok("Mentee updated");
        }

        [HttpPut("disable")]
        public IActionResult Disable(string menteeId)
        {
            if (menteeId == null)
            {
                return BadRequest("MenteeId must not be null.");
            }

            int result = _mentee.ChangeStatus(menteeId, true);

            if (result == 0)
            {
                return BadRequest("Faulthy mentee info.");
            }
            if (result == 1)
            {
                return NotFound("Mentee not found");
            }

            return Ok("Mentee is disabled.");
        }

        [HttpPut("activate")]
        public IActionResult Activate(string menteeId)
        {
            if (menteeId == null)
            {
                return BadRequest("MenteeId must not be null.");
            }

            int result = _mentee.ChangeStatus(menteeId, false);

            if (result == 0)
            {
                return BadRequest("Faulthy mentee info.");
            }
            if (result == 1)
            {
                return NotFound("Mentee not found");
            }

            return Ok("Mentee is activated.");
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(string menteeId)
        {
            if (menteeId == null)
            {
                return BadRequest("Mentee info must not be null");
            }

            int result = _mentee.Delete(menteeId);

            if (result == 0)
            {
                return BadRequest("Faulthy mentee info.");
            }
            if (result == 1)
            {
                return NotFound("Mentee not found");
            }

            return Ok("Mentee is deleted.");
        }

    }
}
