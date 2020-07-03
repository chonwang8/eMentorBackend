using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/mentors")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class MentorController : ControllerBase
    {
        #region Classes - Constructors
        protected readonly IMentorService _mentor;
        public MentorController(IMentorService service)
        {
            _mentor = service;
        }
        #endregion
        

        [HttpGet]
        public IActionResult GetAll(string size, string index, string asc)
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

            #endregion

            paging = new GetAllDTO
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                IsAscending = false
            };

            List<MentorViewModel> result = _mentor.GetAll(paging).ToList();
            if (result == null || result.Count == 0)
            {
                return Ok("There are no users in the system");
            }
            return Ok(result);
        }

        [HttpGet("{mentorId}")]
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

        [HttpPut("status")]
        public IActionResult ChangeStatus(string mentorId, bool isDisable)
        {
            if (mentorId == null)
            {
                return BadRequest("MentorId must not be null.");
            }

            int result = _mentor.ChangeStatus(mentorId, isDisable);

            if (result == 0)
            {
                return BadRequest("Faulthy mentor info.");
            }
            if (result == 1)
            {
                return NotFound("Mentor not found");
            }

            return isDisable ? Ok("Mentor is disabled.")
                : Ok("Mentor is enabled.");
        }

        [HttpDelete("{userId}")]
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
