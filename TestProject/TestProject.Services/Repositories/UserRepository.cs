using TestProject.Entities;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Services.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public string GetUserQuery(string userName)
        {
            return $"SELECT * FROM \"User\" WHERE \"Name\" = \"{userName}\"";
        }

        public string GetUserQuery(string userName, string password)
        {
            return $"SELECT * FROM \"User\" WHERE \"Name\" = \"{userName}\" AND \"Password\" = \"{password}\"";
        }
    }
}
