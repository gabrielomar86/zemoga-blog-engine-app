using System;
using BlogEngineApp.core.dto;
using BlogEngineApp.core.extensions;
using BlogEngineApp.core.interfaces;
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
        public IActionResult Create([FromBody] PostDto postDto)
        {
            _creationPostFlowNotifier.CreatePost(postDto);
            return Created(nameof(Create), postDto);
        }

        [HttpPatch]
        [Route("{postId}/approve")]
        public IActionResult Approve(Guid postId) => Ok(_postService.Approve(postId));

        [HttpPatch]
        [Route("{postId}/reject")]
        public IActionResult Reject(Guid postId) => Ok(_postService.Reject(postId));

        [HttpGet]
        [Route("{postId}")]
        public IActionResult Get(Guid postId) => Ok(_postService.GetById(postId));

        [HttpGet]
        public IActionResult Get() => Ok(_postService.GetAll());

        [HttpGet]
        [Route("pendings")]
        public IActionResult GetPendings([FromQuery] string userId) => Ok(_postService.GetAllPending(userId));

        [HttpGet]
        [Route("approved")]
        public IActionResult GetApproved([FromQuery] string userId) => Ok(_postService.GetAllApproved(userId));

        [HttpGet]
        [Route("rejected")]
        public IActionResult GetRejected([FromQuery] string userId) => Ok(_postService.GetAllRejected(userId));

    }

    class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
