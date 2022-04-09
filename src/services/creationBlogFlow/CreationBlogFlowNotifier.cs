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
            Notify(PostStatus.Create, postDto);
        }

        public void PostCreated(PostDto postDto)
        {
            Notify(PostStatus.Created, postDto);
        }

        public void PostChangedToPending(PostDto postDto)
        {
            Notify(PostStatus.Pending, postDto);
        }

        private void Notify(PostStatus status, object payload)
        {
            _mediator.Publish(new CreationPostFlowCommand { Status = status, Payload = payload });
        }
    }
}