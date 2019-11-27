using System.Linq;

using DataAccess.Repositories.Interfaces;
using Entities;

namespace DataAccess.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
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
