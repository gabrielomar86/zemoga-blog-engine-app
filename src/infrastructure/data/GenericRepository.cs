using System;
using System.Linq;
using System.Linq.Expressions;
using BlogEngineApp.core.interfaces.contratos;
using Microsoft.EntityFrameworkCore;

namespace BlogEngineApp.infrastructure.data
{
    public class GenericRepository<TEntidad> : IGenericRepository<TEntidad> where TEntidad : class
    {
        protected readonly BlogEngineAppContext _blogEngineAppContext;

        public GenericRepository(BlogEngineAppContext blogEngineAppContext)
        {
            _blogEngineAppContext = blogEngineAppContext;
        }

        public void Update(TEntidad entidad)
        {
            _blogEngineAppContext.Set<TEntidad>().Update(entidad);
            _blogEngineAppContext.SaveChanges();
        }

        public IQueryable<TEntidad> FindByCondition(Expression<Func<TEntidad, bool>> expresion)
        {
            return _blogEngineAppContext.Set<TEntidad>().Where(expresion).AsNoTracking();
        }

        public void Delete(TEntidad entidad)
        {
            _blogEngineAppContext.Set<TEntidad>().Remove(entidad);
        }

        public void Insert(TEntidad entidad)
        {
            _blogEngineAppContext.Set<TEntidad>().Add(entidad);
            _blogEngineAppContext.SaveChanges();
        }

        public IQueryable<TEntidad> GetAll()
        {
            return _blogEngineAppContext.Set<TEntidad>().AsNoTracking();
        }
    }
}