using TestProject.Entities;

namespace TestProject.Services.Repositories
{
    public static class Queries
    {
        private const string SelectFromUserTemplate = "SELECT * FROM User";

        public static string GetUserQuery(string userName)
        {
            return $"SELECT * FROM \"User\" WHERE \"Name\" = \"{userName}\"";
        }

        public static string GetUserQuery(User user)
        {
            return $"SELECT * FROM \"User\" WHERE \"Name\" = \"{user.Name}\" AND \"Password\" = \"{user.Password}\"";
        }
    }
}
