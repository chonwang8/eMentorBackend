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
        public IActionResult GetAllComment(string size, string index, string asc)
        {
            int pageSize, pageIndex;
            bool IsAscended = false;
            GetAllDTO request = null;

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

            #endregion Set default paging values if null or empty input

            request = new GetAllDTO
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                IsAscending = false
            };

            return Ok(_service.GetAllComment(request));
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