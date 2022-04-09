using System.Collections.Generic;
using BlogEngineApp.core.entities;
using BlogEngineApp.core.enums;
using BlogEngineApp.core.interfaces.contratos;

namespace BlogEngineApp.infrastructure.data
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(BlogEngineAppContext blogEngineAppContext) : base(blogEngineAppContext)
        {

        }

        public IEnumerable<Blog> FindByStatus(BlogStatus status)
        {
            throw new System.NotImplementedException();
        }
    }
}