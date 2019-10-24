using DataAccess.Context;
using Entities;
using Repositories.Interfaces;
using System.Linq;

namespace Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TodoListContext context) : base(context)
        {
        }
        
        public User Find(string username)
        {
            User user = _dbSet.Where(u => u.Name == username).FirstOrDefault();
            return user;
        }

        public User Find(string username, string password)
        {
            User user = _dbSet.Where(u => u.Name == username && u.Password == password).FirstOrDefault();
            return user;
        }
    }
}
