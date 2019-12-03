using Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User FindById(string username);

        User Find(string username, string password);
    }
}
