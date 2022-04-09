using System;
using System.Linq;
using System.Linq.Expressions;

namespace BlogEngineApp.core.interfaces.contratos
{
    /// <summary>
    /// Repositorio Genérico
    /// </summary>
    /// <typeparam name="TEntidad"></typeparam>
    public interface IGenericRepository<TEntidad>
    {
        /// <summary>
        /// Método que permite el retorno de una colección de la entidad
        /// </summary>
        /// <returns>Retorna todos los elementos de tipo entidad</returns>
        IQueryable<TEntidad> GetAll();

        /// <summary>
        /// Método que permimte obtener un filtrado según la expresión ingresada
        /// </summary>
        /// <param name="expresion">Expresión para el filtrado de la información</param>
        /// <returns>Obtiene informacón filtrada según la expresión ingresada</returns>
        IQueryable<TEntidad> FindByCondition(Expression<Func<TEntidad, bool>> expresion);

        /// <summary>
        /// Método que permite la inserción de un registro
        /// </summary>
        /// <param name="entidad">Entidad</param>
        void Insert(TEntidad entidad);

        /// <summary>
        /// Método que permite la modificación de un registro
        /// </summary>
        /// <param name="entidad">Entidad</param>
        void Update(TEntidad entidad);

        /// <summary>
        /// Método que permite la eliminación de un registro
        /// </summary>
        /// <param name="entidad">Entidad</param>
        void Delete(TEntidad entidad);
    }
}