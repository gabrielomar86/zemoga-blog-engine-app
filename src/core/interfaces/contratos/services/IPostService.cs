using System;
using System.Collections.Generic;
using BlogEngineApp.core.dto;
using BlogEngineApp.core.presenter;

namespace BlogEngineApp.core.interfaces
{
    public interface IPostService
    {
        PostDto CreatePost(PostDto postDto);
        PostPresenter GetPostById(Guid id);
        PostDto ChangePostToCreatedStatus(Guid postId);
        PostDto ChangePostToPendingStatus(Guid postId);
        PostDto ChangePostToRejectStatus(Guid postId);
        PostDto ChangePostToApproveStatus(Guid postId);
        IEnumerable<PostPresenter> GetAllPosts(string userId = null);
        IEnumerable<PostPendingPresenter> GetAllPostsPending(string userId = null);
        IEnumerable<PostPresenter> GetAllPostsApproved(string userId = null);
        IEnumerable<PostPresenter> GetAllPostsRejected(string userId = null);
    }
}