using System;
using System.Linq;
using BlogEngineApp.core.dto;
using BlogEngineApp.core.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogEngineApp.api.Controllers
{
    [ApiController]
    [Route("comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ILogger<CommentController> logger,
                                ICommentService commentService)
        {
            _logger = logger;
            _commentService = commentService;
        }

        [HttpGet]
        [Route("{postId}")]
        public IActionResult GetAllComments(Guid postId)
        {
            return Ok(_commentService.GetAllCommentsByPostId(postId));
        }

        [HttpPost]
        [Route("{postId}")]
        public IActionResult CreateComment(Guid postId, [FromBody] CommentDto commentDto)
        {
            var response = _commentService.CreateComment(new CommentDto
            {
                Detail = commentDto.Detail,
                PostId = postId,
                UserId = GetUserId()
            });

            return Created(nameof(CreateComment), response);
        }

        private string GetUserId()
        {
            try
            {
                var context = HttpContext;
                var claims = context.User.Claims;
                return claims.First(x => x.Type == "id").Value;
            }
            catch
            {
                return null;
            }
        }

    }

}
