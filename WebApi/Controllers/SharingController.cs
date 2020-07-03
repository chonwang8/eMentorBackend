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
    [Route("api/sharings")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class SharingController : ControllerBase
    {
        #region Classes - Constructors
        protected readonly ISharingService _sharing;
        public SharingController(ISharingService service)
        {
            _sharing = service;
        }
        #endregion

        #region RESTful APIs
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

            List<SharingInfoViewModel> result = _sharing.GetAll(paging).ToList();
            if (result == null || result.Count == 0)
            {
                return Ok("There are no sharings in the system");
            }
            return Ok(result);
        }

        [HttpGet("{sharingId}")]
        public IActionResult GetById(string sharingId)
        {
            if (sharingId == null)
            {
                return BadRequest("Sharing info must not be null");
            }

            List<SharingViewModel> result = _sharing.GetById(sharingId).ToList();

            if (result == null || result.Count == 0)
            {
                return NotFound("No sharing(s) with ID " + sharingId + " found.");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Insert(SharingViewModel sharingViewModel)
        {
            if (sharingViewModel == null)
            {
                return BadRequest("Sharing info must not be null");
            }

            int result = _sharing.Insert(sharingViewModel);

            if (result == 0)
            {
                return BadRequest("Faulthy sharing info.");
            }
            if (result == 1)
            {
                return BadRequest("This sharing is already existed");
            }

            return Ok("Sharing Created");
        }

        [HttpPut]
        public IActionResult Update(SharingViewModel sharingViewModel)
        {
            if (sharingViewModel == null)
            {
                return BadRequest("Sharing info must not be null");
            }

            int result = _sharing.Insert(sharingViewModel);

            if (result == 0)
            {
                return BadRequest("Faulthy sharing info.");
            }
            if (result == 1)
            {
                return NotFound("Sharing not found");
            }

            return Ok("Sharing information updated");
        }

        [HttpPut("status")]
        public IActionResult ChangeStatus(string sharingId, bool isDisable)
        {
            if (sharingId == null)
            {
                return BadRequest("SharingId must not be null.");
            }

            int result = _sharing.ChangeStatus(sharingId, isDisable);

            if (result == 0)
            {
                return BadRequest("Faulthy SharingId.");
            }
            if (result == 1)
            {
                return NotFound("Sharing not found");
            }

            return isDisable ? Ok("Sharing is disabled.") 
                : Ok("Sharing is enabled.");
        }

        [HttpDelete("{sharingId}")]
        public IActionResult Delete(string sharingId)
        {
            if (sharingId == null)
            {
                return BadRequest("Sharing info must not be null");
            }

            int result = _sharing.Delete(sharingId);

            if (result == 0)
            {
                return BadRequest("Faulthy sharing info.");
            }
            if (result == 1)
            {
                return NotFound("Sharing was not found");
            }

            return Ok("Sharing is deleted.");
        }


        #region Spare APIs without appropriate methods to call

        //[HttpPut("disable")]
        //public IActionResult Disable(string sharingId)
        //{
        //    if (sharingId == null)
        //    {
        //        return BadRequest("SharingId must not be null.");
        //    }

        //    int result = _sharing.ChangeStatus(sharingId, true);

        //    if (result == 0)
        //    {
        //        return BadRequest("Faulthy sharing info.");
        //    }
        //    if (result == 1)
        //    {
        //        return NotFound("Sharing was not found");
        //    }

        //    return Ok("Sharing is disabled.");
        //}



        //[HttpPut("activate")]
        //public IActionResult Activate(string sharingId)
        //{
        //    if (sharingId == null)
        //    {
        //        return BadRequest("SharingId must not be null.");
        //    }

        //    int result = _sharing.ChangeStatus(sharingId, false);

        //    if (result == 0)
        //    {
        //        return BadRequest("Faulthy sharing info.");
        //    }
        //    if (result == 1)
        //    {
        //        return NotFound("Sharing was not found");
        //    }

        //    return Ok("Sharing is activated.");
        //}
        #endregion


        #endregion

    }
}
