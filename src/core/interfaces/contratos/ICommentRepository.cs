using System;
using System.Collections.Generic;
using BlogEngineApp.core.entities;

namespace BlogEngineApp.core.interfaces
{
    /// <summary>
    /// Post Comments Repository
    /// </summary>
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        IEnumerable<Comment> FindCommentByPostId(Guid postId);
    }
}