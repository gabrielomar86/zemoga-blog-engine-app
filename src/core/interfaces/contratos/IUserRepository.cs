using BlogEngineApp.core.entities;

namespace BlogEngineApp.core.interfaces
{
    /// <summary>
    /// User Repository
    /// </summary>
    public interface IUserRepository : IGenericRepository<User>
    {
        User ValidateUser(string username, string password);
    }
}