using System.Threading.Tasks;

using TestProject.Entities;

namespace TestProject.Services.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<TodoItem>
    {
        Task<TodoItem> GetUser(string userName);

        Task<TodoItem> GetUser(string userName, string password);
    }
}
