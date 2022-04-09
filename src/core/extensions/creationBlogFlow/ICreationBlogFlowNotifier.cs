using BlogEngineApp.core.dto;

namespace BlogEngineApp.core.extensions
{
    public interface ICreationPostFlowNotifier
    {
        void CreatePost(PostDto postDto);
        void PostCreated(PostDto postDto);
        void PostChangedToPending(PostDto postDto);

    }
}
