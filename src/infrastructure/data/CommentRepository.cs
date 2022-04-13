using System;
using System.Collections.Generic;
using System.Linq;
using BlogEngineApp.core.entities;
using BlogEngineApp.core.enums;
using BlogEngineApp.core.interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogEngineApp.infrastructure.data
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogEngineAppContext postEngineAppContext) : base(postEngineAppContext)
        {

        }

        public IEnumerable<Comment> FindCommentByPostId(Guid postId)
        {
            return FindByCondition(comment => comment.PostId == postId)
                .OrderByDescending(comment => comment.CreationDate)
                .Include(comment => comment.Post);
        }
    }
}