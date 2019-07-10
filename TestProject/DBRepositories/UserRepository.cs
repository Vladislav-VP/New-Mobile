using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Entity;
using TestProject.Repositories.Interfaces;

namespace TestProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> FindUserByName(string userName)
        {
            var userTable = new GenericRepository<User>().GetTable<User>();
            return await userTable
                .Where(user => user.UserName == userName)
                .FirstOrDefaultAsync();
        }
    }
}
