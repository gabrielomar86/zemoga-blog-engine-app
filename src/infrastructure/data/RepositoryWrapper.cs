using BlogEngineApp.core.interfaces;

namespace BlogEngineApp.infrastructure.data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly BlogEngineAppContext _blogEngineAppContext;
        private IPostRepository _postRepository;
        private IUserRepository _userRepository;
        private ICommentRepository _commentRepository;

        public RepositoryWrapper(BlogEngineAppContext blogEngineAppContext)
        {
            _blogEngineAppContext = blogEngineAppContext;
        }

        public RepositoryWrapper()
        {
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

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_blogEngineAppContext);
                }
                return _userRepository;
            }
        }

        public ICommentRepository CommentRepository
        {
            get
            {
                if (_commentRepository == null)
                {
                    _commentRepository = new CommentRepository(_blogEngineAppContext);
                }
                return _commentRepository;
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