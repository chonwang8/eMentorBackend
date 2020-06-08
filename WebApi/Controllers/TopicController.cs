﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/topics")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        protected readonly ITopicService _topic;
        public TopicController(ITopicService service)
        {
            _topic = service;
        }



        [HttpGet]
        public IActionResult GetAll()
        {
            List<TopicViewModel> result = _topic.GetAll().ToList();
            if (result == null || result.Count == 0)
            {
                return Ok("There are no topics in the system");
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string topicId)
        {
            if (topicId == null)
            {
                return BadRequest("User info must not be null");
            }

            List<TopicViewModel> result = _topic.GetById(topicId).ToList();

            if (result == null || result.Count == 0)
            {
                return NotFound("No topic with ID " + topicId + " was found.");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Insert(TopicViewModel topic)
        {
            if (topic == null)
            {
                return BadRequest("Topic info must not be null");
            }

            int result = _topic.Insert(topic);

            if (result == 0)
            {
                return BadRequest("Faulthy topic info.");
            }
            if (result == 1)
            {
                return BadRequest("This topic is already in the system");
            }

            return Ok("Inserted topic " + topic.TopicName);
        }

        [HttpPut("{id}")]
        public IActionResult Update(TopicViewModel topic)
        {
            if (topic == null)
            {
                return BadRequest("User info must not be null");
            }

            int result = _topic.Update(topic);

            if (result == 0)
            {
                return BadRequest("Faulthy user info.");
            }
            if (result == 1)
            {
                return NotFound("User not found");
            }

            return Ok("Updated topic " + topic.TopicName);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string topicId)
        {
            if (topicId == null)
            {
                return BadRequest("User info must not be null");
            }

            int result = _topic.Delete(topicId);

            if (result == 0)
            {
                return BadRequest("Faulthy topic info.");
            }
            if (result == 1)
            {
                return NotFound("Topic not found");
            }

            return Ok("Topic is deleted.");
        }
    }
}