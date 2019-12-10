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
            User user = _dbSet.Where(u => u.UserName == username).FirstOrDefault();
            return user;
        }
        
        public User FindById(string id)
        {
            User user = _dbSet.Where(u => u.Id == id).FirstOrDefault();
            return user;
        }
    }
}
