using System;
using System.Collections.Generic;
using BlogEngineApp.core.dto;
using BlogEngineApp.core.presenter;

namespace BlogEngineApp.core.interfaces
{
    public interface IPostService
    {
        PostDto Create(PostDto postDto);
        PostDto GetById(Guid id);
        PostDto Pending(Guid postId);
        PostDto Reject(Guid postId);
        PostDto Approve(Guid postId);
        IEnumerable<PostDto> GetAll(string userId = null);
        IEnumerable<PostPresenter> GetAllPending(string userId = null);
        IEnumerable<PostDto> GetAllApproved(string userId = null);
        IEnumerable<PostDto> GetAllRejected(string userId = null);
    }
}