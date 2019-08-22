using TestProject.Entities;

namespace TestProject.Services.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        string GetUserQuery(string userName);

        string GetUserQuery(string userName, string password);
    }
}
