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

            List<MenteeViewModel> result = _mentee.GetAll(paging).ToList();
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
