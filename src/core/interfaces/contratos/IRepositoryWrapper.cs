namespace BlogEngineApp.core.interfaces
{
    public interface IRepositoryWrapper
    {
        /// <summary>
        /// Repository Wrapper
        /// </summary>
        IBlogRepository BlogEngineAppRepository { get; }


        /// <summary>
        /// Guarda la accion en base de datos
        /// </summary>
        void Save();

        /// <summary>
        /// Inicia la transaccion
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Confirma la transaccion
        /// </summary>
        void ConfirmTransaction();

        /// <summary>
        /// Revierte la transaccion
        /// </summary>
        void RollbackTransaction();
    }
}