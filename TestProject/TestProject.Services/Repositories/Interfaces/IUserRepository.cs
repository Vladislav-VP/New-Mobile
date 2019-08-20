using System.Threading.Tasks;

using TestProject.Entities;

namespace TestProject.Services.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> UserExists(string userName);
    }
}
