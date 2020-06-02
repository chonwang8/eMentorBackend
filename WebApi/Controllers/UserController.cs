﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterModel user)
        {
            string result = _service.Register(user);
            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginModel user)
        {
            string result = _service.Login(user);
            return Ok(result);
        }
    }
}