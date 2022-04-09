using System;
using System.Linq;
using System.Linq.Expressions;

namespace BlogEngineApp.core.interfaces
{
    /// <summary>
    /// Generic Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IGenericRepository<TEntity>
    {
        /// <summary>
        /// Returns entity by id
        /// </summary>
        /// <returns>Returns entity by id</returns>
        TEntity GetById(Guid id);

        /// <summary>
        /// Returns entity collection
        /// </summary>
        /// <returns>Returns entity collection</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Returns a filtered entity
        /// </summary>
        /// <param name="expresion">filter expression</param>
        /// <returns>Returns a filtered entity</returns>
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expresion);

        /// <summary>
        /// Insert a entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(TEntity entity);

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(TEntity entity);
    }
}