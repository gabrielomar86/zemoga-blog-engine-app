using BlogEngineApp.core.dto;

namespace BlogEngineApp.core.extensions
{
    public interface ICreationPostFlowNotifier
    {
        void CreatePost(PostDto postDto);
        void UpdatePostToCreatedStatus(PostDto postDto);
        void PostChangedToCreatedStatus(PostDto postDto);
        void UpdatePostToPendingStatus(PostDto postDto);
        void PostChangedToPendingStatus(PostDto postDto);

    }
}
