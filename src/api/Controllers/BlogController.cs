﻿using System;
using System.Threading.Tasks;
using BlogEngineApp.core.dto;
using BlogEngineApp.core.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogEngineApp.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogEngineAppService;
        private readonly ILogger<BlogController> _logger;

        public BlogController(ILogger<BlogController> logger,
                                IBlogService blogEngineAppService)
        {
            _logger = logger;
            _blogEngineAppService = blogEngineAppService;
            _logger.LogInformation("BlogController created");
        }

        [HttpPost]
        public IActionResult Create([FromBody] BlogDto blogDto)
        {
            return Ok(_blogEngineAppService.Create(blogDto));
        }

        [HttpGet]
        public IActionResult Get() => Ok(_blogEngineAppService.GetAll());

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(Guid id) => Ok(_blogEngineAppService.GetById(id));

    }

    class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
