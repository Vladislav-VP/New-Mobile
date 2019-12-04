using Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User FindByName(string username);

        User FindById(string id);

        User Find(string username, string password);

        void Delete(string id);
    }
}
