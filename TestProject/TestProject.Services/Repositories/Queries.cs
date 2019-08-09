using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Entities;

namespace TestProject.Services.Repositories
{
    public static class Queries
    {
        private const string SelectFromUserTemplate = "SELECT * FROM User";

        public static string GetUserByNameQuery(string userName)
        {
            return $"SELECT * FROM \"User\" WHERE \"Name\" = \"{userName}\"";
        }

        public static string GetUserQuery(User user)
        {
            return $"SELECT * FROM \"User\" WHERE \"Name\" = \"{user.Name}\" AND \"Password\" = \"{user.Password}\"";
        }
    }
}
