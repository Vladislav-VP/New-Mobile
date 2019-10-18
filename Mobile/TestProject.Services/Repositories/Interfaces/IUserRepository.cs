using System.Threading.Tasks;

using TestProject.Entities;

namespace TestProject.Services.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUser(string userName);

        Task<User> GetUser(string userName, string password);
    }
}
