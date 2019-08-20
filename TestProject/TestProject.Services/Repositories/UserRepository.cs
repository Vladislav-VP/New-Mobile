using System.Threading.Tasks;

using TestProject.Entities;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Services.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public async Task<bool> UserExists(string userName)
        {
            string query = Queries.GetUserQuery(userName);
            var user = await FindWithQuery<User>(query);
            return user != null;
        }
    }
}
