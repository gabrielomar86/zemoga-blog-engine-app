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
            try
            {
                var postDto = (PostDto)notification.Payload;

                switch (notification.Status)
                {
                    case PostStatus.CreatePost:
                        Console.WriteLine("=======> Creating Post");
                        _postService.CreatePost(postDto);
                        break;
                    case PostStatus.UpdatePostToCreatedStatus:
                        Console.WriteLine("=======> Post Created --> Update Post To Created Status");
                        _postService.ChangePostToCreatedStatus(postDto.Id);
                        break;
                    case PostStatus.PostCreated:
                        Console.WriteLine("=======> Changing Post To Pending Status");
                        _postService.ChangePostToPendingStatus(postDto.Id);
                        break;
                    case PostStatus.PostPending:
                        Console.WriteLine("=======> Post Changed To Pending Status");
                        break;
                    default:
                        Console.WriteLine("=======> No handler for status: " + notification.Status);
                        break;
                }
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"===> Error Post Creation StackTrace: {ex.StackTrace}");
                Console.WriteLine($"===> Error Post Creation InnerException: {ex.InnerException?.Message}");
                return Task.CompletedTask;
            }
        }

    }
}