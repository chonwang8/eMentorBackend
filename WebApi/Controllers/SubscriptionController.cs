using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/subscription")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        protected readonly ISubscriptionService _service;
        public SubscriptionController(ISubscriptionService service)
        {
            _service = service;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            List<SubscriptionViewModel> result = _service.GetAll().ToList();
            if (result == null || result.Count == 0)
            {
                return Ok("There are no subscription in the system");
            }
            return Ok(result);
        }


        [HttpPost("{subscriptionId}")]
        public IActionResult GetById(string subscriptionId)
        {
            if (subscriptionId == null)
            {
                return BadRequest("Subscription ID info must not be null");
            }

            List<SubscriptionViewModel> result = _service.GetById(subscriptionId).ToList();

            if (result == null || result.Count == 0)
            {
                return NotFound("No Subscription with Id " + subscriptionId + " found.");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Insert(SubscriptionViewModel subscriptionViewModel)
        {
            if (subscriptionViewModel == null)
            {
                return BadRequest("Subscription info must not be null");
            }

            int result = _service.Insert(subscriptionViewModel);

            if (result == 0)
            {
                return BadRequest("Faulthy Subscription info.");
            }
            if (result == 1)
            {
                return BadRequest("This Subscription already existed");
            }

            return Ok("Subscription Inserted");
        }

        [HttpPut]
        public IActionResult Update(SubscriptionViewModel subscriptionViewModel)
        {
            if (subscriptionViewModel == null)
            {
                return BadRequest("Subscription info must not be null");
            }

            int result = _service.Insert(subscriptionViewModel);

            if (result == 0)
            {
                return BadRequest("Faulthy Subscription info.");
            }
            if (result == 1)
            {
                return NotFound("Subscription not found");
            }

            return Ok("Subscription updated");
        }

        [HttpPut("status")]
        public IActionResult ChangeStatus(string subscriptionId, bool isDisable)
        {
            if (subscriptionId == null)
            {
                return BadRequest("SubscriptionId must not be null.");
            }

            int result = _service.ChangeStatus(subscriptionId, isDisable);

            if (result == 0)
            {
                return BadRequest("Faulthy SubscriptionId.");
            }
            if (result == 1)
            {
                return NotFound("Subscription not found");
            }

            return isDisable ? Ok("Subscription is disabled.")
                : Ok("Subscription is enabled.");
        }

        [HttpDelete("{menteeId}")]
        public IActionResult Delete(string menteeId)
        {
            if (menteeId == null)
            {
                return BadRequest("Mentee info must not be null");
            }

            int result = _service.Delete(menteeId);

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
