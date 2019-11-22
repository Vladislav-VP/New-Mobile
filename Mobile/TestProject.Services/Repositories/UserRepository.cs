using System.Threading.Tasks;
using TestProject.Entities;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Services.Repositories
{
    public class UserRepository : BaseRepository<TodoItem>, IUserRepository
    {
        public async Task<TodoItem> GetUser(string userName)
        {
            string query = $"SELECT * FROM \"User\" WHERE \"Name\" = \"{userName}\"";
            return await FindWithQuery(query);
        }

        public async Task<TodoItem> GetUser(string userName, string password)
        {
            string query = $"SELECT * FROM \"User\" WHERE \"Name\" = \"{userName}\" AND \"Password\" = \"{password}\"";
            return await FindWithQuery(query);
        }
    }
}
