using System.Collections.Generic;
using System.Linq;
using BlogEngineApp.core.entities;
using BlogEngineApp.core.enums;
using BlogEngineApp.core.interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogEngineApp.infrastructure.data
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BlogEngineAppContext postEngineAppContext) : base(postEngineAppContext)
        {

        }

        public User ValidateUser(string username, string password)
        {
            return FindByCondition(user => user.UserName == username && user.Password == password)
                .Include(user => user.Posts)
                .FirstOrDefault();
        }
    }
}