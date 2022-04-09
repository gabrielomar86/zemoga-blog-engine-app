using System.Collections.Generic;
using BlogEngineApp.core.entities;
using BlogEngineApp.core.enums;
using BlogEngineApp.core.interfaces;

namespace BlogEngineApp.infrastructure.data
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(BlogEngineAppContext blogEngineAppContext) : base(blogEngineAppContext)
        {

        }

        public IEnumerable<Blog> FindByBlogStatus(BlogStatus status)
        {
            return FindByCondition(blog => blog.Status == status);
        }

        public IEnumerable<Blog> FindByBlogStatusAndUserId(BlogStatus status, string userId)
        {
            return FindByCondition(blog => blog.Status == status && blog.UserId == userId);
        }
    }
}