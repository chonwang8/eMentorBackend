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
    [Route("api/sharings")]
    [ApiController]
    public class SharingController : ControllerBase
    {
        protected readonly ISharingService _sharing;
        public SharingController(ISharingService service)
        {
            _sharing = service;
        }


        #region RESTful APIs
        [HttpGet]
        public IActionResult GetAll(GetAllDTO request)
        {
            List<SharingViewModel> result = _sharing.GetAll(request).ToList();
            if (result == null || result.Count == 0)
            {
                return Ok("There are no sharings in the system");
            }
            return Ok(result);
        }


        [HttpPost("{id}")]
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


        [HttpDelete("{id}")]
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
