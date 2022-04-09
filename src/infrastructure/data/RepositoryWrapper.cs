using BlogEngineApp.core.interfaces;

namespace BlogEngineApp.infrastructure.data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly BlogEngineAppContext _blogEngineAppContext;
        private IBlogRepository _blogRepository;

        public RepositoryWrapper(BlogEngineAppContext blogEngineAppContext)
        {
            _blogEngineAppContext = blogEngineAppContext;
        }

        public IBlogRepository BlobRepository
        {
            get
            {
                if (_blogRepository == null)
                {
                    _blogRepository = new BlogRepository(_blogEngineAppContext);
                }
                return _blogRepository;
            }
        }

        public void ConfirmTransaction()
        {
            _blogEngineAppContext.CommitTransaction();
        }

        public void Save()
        {
            _blogEngineAppContext.SaveChanges();
        }

        public void BeginTransaction()
        {
            _blogEngineAppContext.BeginTransaction();
        }

        public void RollbackTransaction()
        {
            _blogEngineAppContext.RollbackTransaction();
        }
    }
}