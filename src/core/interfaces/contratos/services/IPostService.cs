using System;
using System.Collections.Generic;
using BlogEngineApp.core.dto;
using BlogEngineApp.core.presenter;

namespace BlogEngineApp.core.interfaces
{
    public interface IPostService
    {
        PostDto CreatePost(PostDto postDto);
        PostDto GetPostById(Guid id);
        PostDto ChangePostToCreatedStatus(Guid postId);
        PostDto ChangePostToPendingStatus(Guid postId);
        PostDto ChangePostToRejectStatus(Guid postId);
        PostDto ChangePostToApproveStatus(Guid postId);
        IEnumerable<PostDto> GetAllPosts(string userId = null);
        IEnumerable<PostPresenter> GetAllPostsPending(string userId = null);
        IEnumerable<PostDto> GetAllPostsApproved(string userId = null);
        IEnumerable<PostDto> GetAllPostsRejected(string userId = null);
    }
}