using System;
using System.Collections.Generic;
using BlogEngineApp.core.entities;
using BlogEngineApp.core.enums;

namespace BlogEngineApp.core.interfaces
{
    /// <summary>
    /// Post Repository
    /// </summary>
    public interface IPostRepository : IGenericRepository<Post>
    {
        IEnumerable<Post> FindPostsByPostStatus(PostStatus status);
        IEnumerable<Post> FindPostsByStatusAndUserId(PostStatus status, string userId);
    }
}