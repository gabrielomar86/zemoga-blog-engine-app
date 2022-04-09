using System;
using System.Collections.Generic;
using BlogEngineApp.core.entities;
using BlogEngineApp.core.enums;

namespace BlogEngineApp.core.interfaces
{
    /// <summary>
    /// Blog Repository
    /// </summary>
    public interface IBlogRepository : IGenericRepository<Blog>
    {
        IEnumerable<Blog> FindByBlogStatus(BlogStatus status);
        IEnumerable<Blog> FindByBlogStatusAndUserId(BlogStatus status, string userId);
    }
}