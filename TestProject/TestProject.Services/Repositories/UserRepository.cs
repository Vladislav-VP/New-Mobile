using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Entities;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Services.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public async Task<bool> UserExists(string userName)
        {
            var users = await GetAllObjects<User>();
            return (await FindUser(userName)) != null;
        }

        public async Task<User> FindUser(string userName)
        {
            var users = await GetAllObjects<User>();
            return users.Where(user => user.Name == userName).FirstOrDefault();
        }
    }
}
