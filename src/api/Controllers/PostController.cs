using System;
using System.Linq;
using BlogEngineApp.core.dto;
using BlogEngineApp.core.extensions;
using BlogEngineApp.core.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogEngineApp.api.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ILogger<PostController> _logger;
        private readonly ICreationPostFlowNotifier _creationPostFlowNotifier;

        public PostController(ILogger<PostController> logger,
                                IPostService postService,
                                ICreationPostFlowNotifier creationPostFlowNotifier)
        {
            _creationPostFlowNotifier = creationPostFlowNotifier;
            _logger = logger;
            _postService = postService;
        }

        [HttpPost]
        [Authorize("OnlyWriter")]
        public IActionResult CreatePost([FromBody] PostDto postDto)
        {
            var postResponse = _postService.CreatePost(new PostDto
            {
                Title = postDto.Title,
                Content = postDto.Content,
                UserId = GetUserId()
            });
            return Created(nameof(UpdatePost), postResponse);
        }

        [HttpPut]
        [Route("{postId}")]
        [Authorize("OnlyWriter")]
        public IActionResult UpdatePost(Guid postId, [FromBody] PostDto postDto)
        {
            var postResponse = _postService.UpdatePost(new PostDto
            {
                Id = postId,
                Title = postDto.Title,
                Content = postDto.Content
            });
            return Ok(postResponse);
        }

        [HttpDelete]
        [Route("{postId}")]
        [Authorize("OnlyEditor")]
        public IActionResult DeletePost(Guid postId)
        {
            _postService.ChangePostToDeleteStatus(postId);
            return Ok();
        }

        [HttpPatch]
        [Authorize("OnlyEditor")]
        [Route("{postId}/approve")]
        public IActionResult Approve(Guid postId) => Ok(_postService.ChangePostToApproveStatus(postId));

        [HttpPatch]
        [Authorize("OnlyEditor")]
        [Route("{postId}/reject")]
        public IActionResult Reject(Guid postId) => Ok(_postService.ChangePostToRejectStatus(postId));

        [HttpGet]
        [Route("{postId}")]
        public IActionResult Get(Guid postId) => Ok(_postService.GetPostById(postId));

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_postService.GetAllPosts(GetUserId()));
        }

        [HttpGet]
        [Authorize("OnlyEditor")]
        [Route("pendings")]
        public IActionResult GetAllPostsPendings([FromQuery] string userId)
        {
            return Ok(_postService.GetAllPostsPending(userId));
        }

        [HttpGet]
        [Route("approved")]
        public IActionResult GetAllPostsApproved([FromQuery] string userId) => Ok(_postService.GetAllPostsApproved(userId));

        [HttpGet]
        [Route("rejected")]
        [Authorize("OnlyWriter")]
        public IActionResult GetAllPostRejected() => Ok(_postService.GetAllPostsRejected(GetUserId()));

        private string GetUserId()
        {
            var context = HttpContext;
            var claims = context.User.Claims;
            return claims.First(x => x.Type == "id").Value;
        }

    }

}
