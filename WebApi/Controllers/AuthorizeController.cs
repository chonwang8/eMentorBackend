using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helper.AdminFunctions.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        protected readonly IAdminLogic _admin;
        public AuthorizeController(IAdminLogic admin)
        {
            _admin = admin;
        }


        [HttpPost]
        public IActionResult AdminLogin(AdminLoginViewModel adminLogin)
        {
            if (adminLogin == null)
            {
                return BadRequest("Bad Input");
            }
            string result = _admin.Login(adminLogin);
            if (result.Equals("Incorrect username or password"))
            {
                return NotFound("Incorrect username or password");
            }

            return Ok(result);
        }
    }
}
