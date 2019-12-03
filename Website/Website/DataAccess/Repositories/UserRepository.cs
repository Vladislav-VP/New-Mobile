using System.Linq;

using DataAccess.Context;
using DataAccess.Repositories.Interfaces;
using Entities;

namespace DataAccess.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TodoListContext context) : base(context)
        {
        }

        public User FindByName(string username)
        {
            var users = _dbSet.ToList();
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
