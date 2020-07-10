using Domain.DTO;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/comments")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class CommentController : ControllerBase
    {
        #region Classes - Constructors
        protected readonly ICommentService _service;

        public CommentController(ICommentService service)
        {
            _service = service;
        }
        #endregion


        [HttpGet]
        public IActionResult GetAllComment()
        {
            return Ok(_service.GetAllComment());
        }

        [HttpGet("{CommentId}")]
        public IActionResult GetCommentById(Guid CommentId)
        {
            if (_service.GetCommentById(CommentId) == null)
            {
                return NotFound("Not found Comment for this ID");
            }
            return Ok(_service.GetCommentById(CommentId));
        }
    }
}