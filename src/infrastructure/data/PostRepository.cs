using System.Collections.Generic;
using System.Linq;
using BlogEngineApp.core.entities;
using BlogEngineApp.core.enums;
using BlogEngineApp.core.interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogEngineApp.infrastructure.data
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(BlogEngineAppContext postEngineAppContext) : base(postEngineAppContext)
        {

        }

        public IEnumerable<Post> FindPostsByPostStatus(PostStatus status)
        {
            return FindByCondition(post => post.Status == status)
                .OrderByDescending(post => post.CreationDate)
                .Include(post => post.User);
        }

        public IEnumerable<Post> FindPostsByStatusAndUserId(PostStatus status, string userId)
        {
            return FindByCondition(post => post.Status == status && post.UserId == userId)
                .OrderByDescending(comment => comment.CreationDate)
                .Include(post => post.User);
        }
    }
}