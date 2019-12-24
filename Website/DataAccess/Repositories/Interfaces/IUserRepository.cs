using Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User FindByName(string username);

        User FindById(string id);
    }
}
