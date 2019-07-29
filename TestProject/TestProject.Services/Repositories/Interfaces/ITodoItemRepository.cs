using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Entities;

namespace TestProject.Services.Repositories.Interfaces
{
    public interface ITodoItemRepository : IBaseRepository<TodoItem>
    {
        Task<IEnumerable<TodoItem>> GetTodoItems(int userId);
    }
}
