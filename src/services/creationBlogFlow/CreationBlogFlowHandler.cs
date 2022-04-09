using System;
using System.Threading;
using System.Threading.Tasks;
using BlogEngineApp.core.dto;
using BlogEngineApp.core.enums;
using BlogEngineApp.core.interfaces;
using MediatR;

namespace BlogEngineApp.services
{
    public class CreationPostFlowHandler : INotificationHandler<CreationPostFlowCommand>
    {
        private readonly IPostService _postService;

        public CreationPostFlowHandler(IPostService postService)
        {
            _postService = postService;
        }

        public Task Handle(CreationPostFlowCommand notification, CancellationToken cancellationToken)
        {
            var postDto = (PostDto)notification.Payload;

            switch (notification.Status)
            {
                case PostStatus.Create:
                    Console.WriteLine("=======> Creating Post");
                    _postService.Create(postDto);
                    break;
                case PostStatus.Created:
                    Console.WriteLine("=======> Post Created");
                    _postService.Pending(postDto.Id);
                    break;
                case PostStatus.Pending:
                    Console.WriteLine("=======> Post Changed To Pending");
                    break;
                default:
                    Console.WriteLine("=======> No handler for status: " + notification.Status);
                    break;
            }
            return Task.CompletedTask;
        }

    }
}