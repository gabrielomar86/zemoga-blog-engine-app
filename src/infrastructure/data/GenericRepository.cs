using System;
using System.Linq;
using System.Linq.Expressions;
using BlogEngineApp.core.interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogEngineApp.infrastructure.data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly BlogEngineAppContext _blogEngineAppContext;

        public GenericRepository(BlogEngineAppContext blogEngineAppContext)
        {
            _blogEngineAppContext = blogEngineAppContext;
        }

        public void Update(TEntity entity)
        {
            _blogEngineAppContext.Set<TEntity>().Update(entity);
            _blogEngineAppContext.SaveChanges();
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expresion)
        {
            return _blogEngineAppContext.Set<TEntity>().Where(expresion).AsNoTracking();
        }

        public void Delete(TEntity entity)
        {
            _blogEngineAppContext.Set<TEntity>().Remove(entity);
        }

        public void Insert(TEntity entity)
        {
            _blogEngineAppContext.Set<TEntity>().Add(entity);
            _blogEngineAppContext.SaveChanges();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _blogEngineAppContext.Set<TEntity>().AsNoTracking();
        }

        public TEntity GetById(Guid id)
        {
            return _blogEngineAppContext.Set<TEntity>().Find(id);
        }
    }
}