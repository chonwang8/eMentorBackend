﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/mentors")]
    [ApiController]
    public class MentorController : ControllerBase
    {
        protected readonly IMentorService _mentor;
        public MentorController(IMentorService service)
        {
            _mentor = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<MentorViewModel> result = _mentor.GetAll().ToList();
            if (result == null || result.Count == 0)
            {
                return Ok("There are no users in the system");
            }
            return Ok(result);
        }


        [HttpPost("{id}")]
        public IActionResult GetById(string mentorId)
        {
            if (mentorId == null)
            {
                return BadRequest("Mentor info must not be null");
            }

            List<MentorViewModel> result = _mentor.GetById(mentorId).ToList();

            if (result == null || result.Count == 0)
            {
                return NotFound("No mentor with mentorId " + mentorId + " found.");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Insert(MentorViewModel mentor)
        {
            if (mentor == null)
            {
                return BadRequest("User info must not be null");
            }

            int result = _mentor.Insert(mentor);

            if (result == 0)
            {
                return BadRequest("Faulthy mentor info.");
            }
            if (result == 1)
            {
                return BadRequest("This user is already a mentor");
            }

            return Ok("Mentor Inserted");
        }

        [HttpPut]
        public IActionResult Update(MentorViewModel mentor)
        {
            if (mentor == null)
            {
                return BadRequest("Mentor info must not be null");
            }

            int result = _mentor.Insert(mentor);

            if (result == 0)
            {
                return BadRequest("Faulthy mentor info.");
            }
            if (result == 1)
            {
                return NotFound("Mentor not found");
            }

            return Ok("Mentor updated");
        }

        [HttpPut("disable")]
        public IActionResult Disable(string mentorId)
        {
            if (mentorId == null)
            {
                return BadRequest("MentorId must not be null.");
            }

            int result = _mentor.ChangeStatus(mentorId, true);

            if (result == 0)
            {
                return BadRequest("Faulthy mentor info.");
            }
            if (result == 1)
            {
                return NotFound("Mentor not found");
            }

            return Ok("Mentor is disabled.");
        }

        [HttpPut("activate")]
        public IActionResult Activate(string mentorId)
        {
            if (mentorId == null)
            {
                return BadRequest("MentorId must not be null.");
            }

            int result = _mentor.ChangeStatus(mentorId, false);

            if (result == 0)
            {
                return BadRequest("Faulthy mentor info.");
            }
            if (result == 1)
            {
                return NotFound("Mentor not found");
            }

            return Ok("Mentor is activated.");
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(string userId)
        {
            if (userId == null)
            {
                return BadRequest("Mentor info must not be null");
            }

            int result = _mentor.Delete(userId);

            if (result == 0)
            {
                return BadRequest("Faulthy mentor info.");
            }
            if (result == 1)
            {
                return NotFound("Mentor not found");
            }

            return Ok("Mentor is deleted.");
        }

    }
}