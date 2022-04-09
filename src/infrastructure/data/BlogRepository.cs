using System.Collections.Generic;
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

        public IEnumerable<Post> FindByPostStatus(PostStatus status)
        {
            return FindByCondition(post => post.Status == status)
                .Include(post => post.User);
        }

        public IEnumerable<Post> FindByPostStatusAndUserId(PostStatus status, string userId)
        {
            return FindByCondition(post => post.Status == status && post.UserId == userId)
                .Include(post => post.User);
        }
    }
}