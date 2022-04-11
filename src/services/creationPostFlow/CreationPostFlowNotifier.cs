using BlogEngineApp.core.dto;
using BlogEngineApp.core.enums;
using BlogEngineApp.core.extensions;
using MediatR;

namespace BlogEngineApp.services
{
    public class CreationPostFlowNotifier : ICreationPostFlowNotifier
    {
        private readonly IMediator _mediator;

        public CreationPostFlowNotifier(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void CreatePost(PostDto postDto)
        {
            Notify(PostStatus.CreatePost, postDto);
        }

        public void UpdatePostToCreatedStatus(PostDto postDto)
        {
            Notify(PostStatus.UpdatePostToCreatedStatus, postDto);
        }

        public void PostChangedToCreatedStatus(PostDto postDto)
        {
            Notify(PostStatus.PostCreated, postDto);
        }

        public void UpdatePostToPendingStatus(PostDto postDto)
        {
            Notify(PostStatus.UpdatePostToPendingStatus, postDto);
        }

        public void PostChangedToPendingStatus(PostDto postDto)
        {
            Notify(PostStatus.PostPending, postDto);
        }

        private void Notify(PostStatus status, object payload)
        {
            _mediator.Publish(new CreationPostFlowCommand { Status = status, Payload = payload });
        }

    }
}