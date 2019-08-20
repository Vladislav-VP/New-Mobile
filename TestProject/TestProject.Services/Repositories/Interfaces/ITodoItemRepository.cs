using System.Collections.Generic;
using System.Threading.Tasks;

using TestProject.Entities;

namespace TestProject.Services.Repositories.Interfaces
{
    public interface ITodoItemRepository : IBaseRepository<TodoItem>
    {
        Task<IEnumerable<TodoItem>> GetTodoItems(int userId);
    }
}
