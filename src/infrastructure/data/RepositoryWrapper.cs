using BlogEngineApp.core.interfaces;

namespace BlogEngineApp.infrastructure.data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly BlogEngineAppContext _blogEngineAppContext;
        private IPostRepository _postRepository;

        public RepositoryWrapper(BlogEngineAppContext blogEngineAppContext)
        {
            _blogEngineAppContext = blogEngineAppContext;
        }

        public IPostRepository PostRepository
        {
            get
            {
                if (_postRepository == null)
                {
                    _postRepository = new PostRepository(_blogEngineAppContext);
                }
                return _postRepository;
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